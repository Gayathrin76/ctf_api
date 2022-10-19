using ctf_api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ctf_api.IDBContext
{
   public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
