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
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }
            try
            {

                await _emailService.SendEmailAsync(request);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error sending email: {ex.Message}");
            }
        }

        [Route("send-code")]
        [HttpPost]
        public async Task<IActionResult> SendEmailToCode([FromBody] SendCodeRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }
            try
            {
                await _emailService.SendEmailAsyncToCode(request);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error sending email: {ex.Message}");
            }
        }
    }
}
