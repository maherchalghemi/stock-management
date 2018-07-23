using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Tahri_Company.Models
{
    public class MessageServices
    {

        public async static Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
         
                var _dispName = "AchrafRk";
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(email);
                myMessage.From = new MailAddress("achrafrejebkaabia@gmail.com", _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;//465
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("achrafrejebkaabia@gmail.com", "bJINPPIK");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
