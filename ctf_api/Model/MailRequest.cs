using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ctf_api.Model
{
    public class MailRequest
    {
        public string Name { get; set; }
        public string ToEmail { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string fromSite { get; set; }
        //public List<IFormFile> Attachments { get; set; }
    }
}
