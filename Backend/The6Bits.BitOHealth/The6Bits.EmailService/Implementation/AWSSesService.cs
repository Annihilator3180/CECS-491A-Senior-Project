using System.Net;
using System.Net.Mail;

namespace The6Bits.EmailService
{
    //add dependency again
    public class AWSSesService
    {
        readonly private string DO_NOT_REPLY_EMAIL = "donotreply@bitohealth.com";
        readonly private string DO_NOT_REPLY_SENDER = "do not reply";
        readonly private int PORT = 587;
        readonly private string HOST = "email-smtp.us-east-1.amazonaws.com";
        readonly private string SMTP_USERNAME = "AKIAUE7KDTT3NT6LWEDD";
        readonly string SMTP_PASSWORD = "BAcAcIHFgOSqH+z6kSbAVb34DlEMnm9oJBUbPJIZevZQ";

        public string SendEmailNoReply(string email, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(DO_NOT_REPLY_EMAIL, DO_NOT_REPLY_SENDER);
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.Body = body;

            using (var client = new SmtpClient(HOST, PORT))
            {
                client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                client.EnableSsl = true;
                try
                {
                    client.Send(message);
                    return "email sent";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
