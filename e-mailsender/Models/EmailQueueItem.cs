namespace e_mailsender.Models
{
    public class EmailQueueItem
    {
        public SendEmailRequest? EmailRequest { get; set; }
        public SendCodeRequest? CodeEmailRequest { get; set; }
    }

}
