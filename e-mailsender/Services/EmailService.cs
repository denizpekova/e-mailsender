using e_mailsender.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace e_mailsender.Services
{
    public class EmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(SendEmailRequest request)
        {
            if (request == null)
            {
                return;
            }

            var message = CreateMessage(request.From, request.To, request.Subject, request.Body, request.IsHtml);
            await SendWithMailKitAsync(message, request.Smtp.Host, request.Smtp.Port, request.Smtp.Username, request.Smtp.Password, request.Smtp.EnableSsl);
        }


        public async Task SendEmailAsyncToCode(SendCodeRequest request)
        {
            if (request == null)
            {
                return;
            }

            var bodyWithCode = request.IsHtml
                ? $"{request.Body}<br/><br/><strong>Code: {request.Code}</strong>"
                : $"{request.Body}{Environment.NewLine}{Environment.NewLine}Code: {request.Code}";

            var message = CreateMessage(request.From, request.To, request.Subject, bodyWithCode, request.IsHtml);
            await SendWithMailKitAsync(message, request.Smtp.Host, request.Smtp.Port, request.Smtp.Username, request.Smtp.Password, request.Smtp.EnableSsl);
        }

        private static MimeMessage CreateMessage(string from, string to, string subject, string body, bool isHtml)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(from));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(isHtml ? "html" : "plain")
            {
                Text = body
            };

            return message;
        }

        private static async Task SendWithMailKitAsync(
            MimeMessage message,
            string host,
            int port,
            string username,
            string password,
            bool enableSsl)
        {
            var socketOption = port switch
            {
                465 => SecureSocketOptions.SslOnConnect,
                587 => SecureSocketOptions.StartTls,
                _ => enableSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(host, port, socketOption);
            await client.AuthenticateAsync(username, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
