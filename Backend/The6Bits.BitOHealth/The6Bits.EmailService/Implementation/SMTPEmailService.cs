using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.EmailService
{
    public class SMTPEmailService : ISMTPEmailServiceShould
        //add dependency again
    {
        public string SendEmail(string email, string subject, string body)
        {
            string sender = "test@bitohealth.com";
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "bitohealth.com",
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential("username", "password"),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(sender, email, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "email sent";
        }
    }
}
