using ctf_api.IDBContext;
using ctf_api.Model;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using MailKit.Net.Smtp;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ctf_api.DBContext
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_mailSettings.Email, _mailSettings.Pass);
            client.Port = _mailSettings.Port; // 25 587
            client.Host = _mailSettings.Host;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_mailSettings.Email, "test");
            mail.To.Add(new MailAddress(mailRequest.ToEmail));
            mail.Subject = mailRequest.Subject;
            string mailBoby = "";
            mailBoby = mailBoby + "Hi, <br/>";
            mailBoby = mailBoby + "Please find the details below: "+ "<br/> <br/>";
            mailBoby = mailBoby + "Name: " + mailRequest.Name + "<br/>";
            mailBoby = mailBoby + "Phone: " + mailRequest.PhoneNumber + "<br/>";
            mailBoby = mailBoby + "Email: " + mailRequest.Email + "<br/>";
            mailBoby = mailBoby + "Description: " + mailRequest.Message + "<br/><br/>";
            mailBoby = mailBoby + "Thanks and Regards,<br/>";
            mailBoby = mailBoby + "Support Team";
            mail.Body = mailBoby;
            mail.IsBodyHtml = true;
            try
            {
                await client.SendMailAsync(mail);
                client.Dispose();
                mail.Dispose();
            }
            catch (Exception ex)
            {
                client.Dispose();
                mail.Dispose();
            }
        }
    }
}
