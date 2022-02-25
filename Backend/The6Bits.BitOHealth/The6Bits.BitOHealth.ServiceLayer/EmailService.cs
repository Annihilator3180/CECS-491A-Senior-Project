using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class EmailService
    {
        string email;
        string subject;
        string body;
        public EmailService(string email, string subject,string body)
        {
           this.email = email;
            this.subject = subject;
            this.body = body;
        }
        public string SendEmail()
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
                MailMessage message = new MailMessage(sender, GetEmail(), GetSubject(), GetBody());
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "email sent";
        }
        public string GetEmail()
        {
            return email;
    }
        public string GetSubject()
        {
            return subject;
        }
        public string GetBody()
        {
            return body;
        }
    }
   
    }
