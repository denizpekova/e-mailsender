namespace e_mailsender.Models
{
    public class SendCodeRequest
    {
        public required SmtpSettings Smtp { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public bool IsHtml { get; set; }
        public required string Code { get; set; }
    }
}
