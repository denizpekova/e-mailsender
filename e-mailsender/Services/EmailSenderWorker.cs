using e_mailsender.Models;

namespace e_mailsender.Services
{
    public sealed class EmailSenderWorker : BackgroundService
    {
        private readonly IEmailQueue _queue;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<EmailSenderWorker> _logger;

        public EmailSenderWorker(
            IEmailQueue queue,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<EmailSenderWorker> logger)
        {
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var item = await _queue.DequeueAsync(stoppingToken);
                const int maxRetries = 3;

                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    try
                    {
                        using var scope = _serviceScopeFactory.CreateScope();
                        var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

                        if (item.EmailRequest is not null)
                        {
                            await emailService.SendEmailAsync(item.EmailRequest);
                            _logger.LogInformation("Mail sent. To: {To}", item.EmailRequest.To);
                        }
                        else if (item.CodeEmailRequest is not null)
                        {
                            await emailService.SendEmailAsyncToCode(item.CodeEmailRequest);
                            _logger.LogInformation("Code mail sent. To: {To}", item.CodeEmailRequest.To);
                        }
                        else
                        {
                            _logger.LogWarning("Queue item skipped because it contains no request payload.");
                        }

                        break;
                    }
                    catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                    {
                        return;
                    }
                    catch (Exception ex) when (attempt < maxRetries)
                    {
                        var recipient = item.EmailRequest?.To ?? item.CodeEmailRequest?.To ?? "unknown";
                        _logger.LogWarning(ex, "Email send failed. Retry {Attempt}/{MaxRetries}. To: {To}", attempt, maxRetries, recipient);
                        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)), stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        var recipient = item.EmailRequest?.To ?? item.CodeEmailRequest?.To ?? "unknown";
                        _logger.LogError(ex, "Email send failed after {MaxRetries} attempts. To: {To}", maxRetries, recipient);
                    }
                }
            }
        }
    }
}
