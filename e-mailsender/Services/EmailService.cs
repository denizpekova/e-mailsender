using e_mailsender.Models;
using System.Net;
using System.Net.Mail;

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
            if (request == null) return;

            using SmtpClient client = new SmtpClient(request.Smtp.Host, request.Smtp.Port)
            {
                Credentials = new NetworkCredential(request.Smtp.Username, request.Smtp.Password),
                EnableSsl = request.Smtp.EnableSsl
            };

            using MailMessage mail = new MailMessage(request.From, request.To, request.Subject, request.Body)
            {
                IsBodyHtml = request.IsHtml
            };

            await client.SendMailAsync(mail);
        }


        public async Task SendEmailAsyncToCode(SendCodeRequest request)
        {
            if (request == null) return;

            using SmtpClient client = new SmtpClient(request.Smtp.Host, request.Smtp.Port)
            {
                Credentials = new NetworkCredential(request.Smtp.Username, request.Smtp.Password),
                EnableSsl = request.Smtp.EnableSsl
            };

            using MailMessage mail = new MailMessage(request.From, request.To, request.Subject, request.Body)
            {
                IsBodyHtml = request.IsHtml
            };

            await client.SendMailAsync(mail);
        }
    }
}
