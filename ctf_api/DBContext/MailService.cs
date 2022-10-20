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
            mail.From = new MailAddress(_mailSettings.Email, "Support Team");
            mail.To.Add(new MailAddress(mailRequest.ToEmail));
            mail.Subject = mailRequest.fromSite+ "- Contact";
            string mailBoby = "";
            mailBoby = mailBoby + "<b>Hi Admin,</b><br/><br/>";
            mailBoby = mailBoby + "A new contact request has been submitted with the following information." + "<br/> <br/>";
            
            mailBoby = mailBoby + "<b>Name:</b><br/>";
            mailBoby = mailBoby + mailRequest.Name + " (" + mailRequest.Email + ")<br/>";
            mailBoby = mailRequest.PhoneNumber!=""? mailBoby + "+91 " + mailRequest.PhoneNumber + "<br/><br/>": mailBoby + "<br/>";
         
            mailBoby = mailBoby + "<b>Subject:</b><br/>";
            mailBoby = mailBoby +  mailRequest.Subject + "<br/><br/>";

            mailBoby = mailBoby + "<b>Message:</b><br/>";
            mailBoby = mailBoby +  mailRequest.Message + "<br/><br/>";

            mailBoby = mailBoby + "Regards,<br/>";
            mailBoby = mailBoby + "Support - " + mailRequest.fromSite;
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
