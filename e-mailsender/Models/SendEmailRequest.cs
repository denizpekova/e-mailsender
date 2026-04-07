using System.ComponentModel.DataAnnotations;

namespace e_mailsender.Models
{
    public class SendEmailRequest
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
    }

    public class SmtpSettings
    {
        [Required(ErrorMessage = "SMTP host zorunludur.")]
        public required string Host { get; set; }

        [Range(1, 65535, ErrorMessage = "SMTP port değeri 1-65535 aralığında olmalıdır.")]
        public int Port { get; set; }

        [Required(ErrorMessage = "SMTP kullanıcı adı zorunludur.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "SMTP şifresi zorunludur.")]
        public required string Password { get; set; }

        public bool EnableSsl { get; set; }
    }
}
