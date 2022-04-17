using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.EmailService;
using The6Bits.SMTPEmailService.Implementation;
using Xunit;

namespace The6Bits.BitOHealth.AccountRecovery.Test
{
    public class EmailServiceShould
    {
        SESConfig _config;
        ISMTPEmailService _ES;

        public EmailServiceShould()
        {
            _ES = new AWSSesService(_config);

        }
        [Fact]
        public void SendEmailTest()
        {
            ///arrange
            string correctMessage = "email sent";
            ///act
            string testEmail = _ES.SendEmailNoReply("spam@bitohealth.com", "Test Email", "Email has been sent");
            //assert
            Assert.Equal(correctMessage, testEmail);
            

        }

    }
}
