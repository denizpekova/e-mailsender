using System.ComponentModel.DataAnnotations;

namespace e_mailsender.Models
{
    public class SendCodeRequest
    {
        public required SmtpSettings Smtp { get; set; }


        [Required(ErrorMessage = "Gönderici adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir gönderici e-posta adresi giriniz.")]
        public required string From { get; set; }

        [Required(ErrorMessage = "Alıcı adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir alıcı e-posta adresi giriniz.")]
        public required string To { get; set; }


        [Required(ErrorMessage = "Konu kısmı boş olamaz.")]
        [MaxLength(100, ErrorMessage = "Konu 100 karakterden uzun olamaz.")]
        public required string Subject { get; set; }

        [Required]
        public required string Body { get; set; }
        public bool IsHtml { get; set; }
        [Required]
        public required string Code { get; set; }
    }
}
