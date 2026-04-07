using System.ComponentModel.DataAnnotations;

namespace e_mailsender.Models
{
    public class SendCodeRequest
    {
        [Required(ErrorMessage = "SMTP ayarları zorunludur.")]
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

        [Required(ErrorMessage = "Mail içeriği zorunludur.")]
        [MinLength(1, ErrorMessage = "Mail içeriği boş olamaz.")]
        public required string Body { get; set; }

        public bool IsHtml { get; set; }

        [Required(ErrorMessage = "Kod zorunludur.")]
        [RegularExpression("^\\d{6}$", ErrorMessage = "Kod 6 haneli sayısal olmalıdır.")]
        public required string Code { get; set; }
    }
}
