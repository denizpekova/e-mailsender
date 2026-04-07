using e_mailsender.Models;
using e_mailsender.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_mailsender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailQueue _emailQueue;

        public EmailController(IEmailQueue emailQueue)
        {
            _emailQueue = emailQueue;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            await _emailQueue.QueueAsync(new EmailQueueItem { EmailRequest = request });
            return Ok("Email sent successfully.");
        }

        [Route("send-code")]
        [HttpPost]
        public async Task<IActionResult> SendEmailToCode([FromBody] SendCodeRequest request)
        {
            await _emailQueue.QueueAsync(new EmailQueueItem { CodeEmailRequest = request });
            return Ok("Email sent successfully.");
        }
    }
}
