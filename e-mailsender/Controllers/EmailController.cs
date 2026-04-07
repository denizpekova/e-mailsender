using e_mailsender.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_mailsender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly Services.EmailService _emailService;

        public EmailController(Services.EmailService emailService)
        {
            _emailService = emailService;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
                await _emailService.SendEmailAsync(request);
                return Ok("Email sent successfully.");        
        }

        [Route("send-code")]
        [HttpPost]
        public async Task<IActionResult> SendEmailToCode([FromBody] SendCodeRequest request)
        {
   
               await _emailService.SendEmailAsyncToCode(request);
               return Ok("Email sent successfully.");
           
        }
    }
}
