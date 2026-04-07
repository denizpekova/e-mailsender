namespace e_mailsender.Models
{
    public class SendEmailRequest
    {
        public required SmtpSettings Smtp { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public bool IsHtml { get; set; }
    }

    public class SmtpSettings
    {
        public required string Host { get; set; }
        public int Port { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}
