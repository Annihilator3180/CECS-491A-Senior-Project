using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.EmailService
{
    public interface ISMTPEmailService
    {
        public string SendEmailNoReply(string email, string subject, string body);
    }
}
