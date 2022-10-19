using ctf_api.IDBContext;
using ctf_api.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ctf_api.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class EmailController : Controller
    {
        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return new JsonResult(new
                {
                    success = true,
                    message = "Message Sent!"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Message failed!"
                });
            }
        }
    }
}
