using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.EmailService
{
    internal interface ISMTPEmailServiceShould
    {
        public string SendEmail(string email, string subject, string body);
    }
}
