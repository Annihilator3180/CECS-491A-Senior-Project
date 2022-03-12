using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;
using Xunit;

namespace The6Bits.BitOHealth.AccountRecovery.Test
{
    public class AccountServiceShould
    {
        AccountService _AS ;

        public AccountServiceShould(){
            _AS = new AccountService(new AccountMsSqlDao("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;"));

        }

        [Fact]
        public void UsernameAndEmailTest()
        {
            string email = "cbass@gmail.com";
            string username = "bossadmin12";
            string predicted = "Email and Username found";

            string actual = _AS.UsernameAndEmailExists(username, email);

            Assert.Equal(actual, predicted);
        }

        [Fact]
        public void IsEnabledTest()
        {
            string username = "bossadmin12";
            string predicted = "enabled";

            string actual = _AS.IsEnabled(username);
            Console.WriteLine(actual);
            Assert.Equal(predicted, actual);
        }
        [Fact]
        public void ValidateRecoveryAttemptsTest()
        {
            string username = "bossadmin12";
            string predicted = "under";

            string actual = _AS.ValidateRecoveryAttempts(username);

            Assert.Equal(actual, predicted);
        }
        [Fact]
        public void UpdateRecoveryAttemptsTest()
        {
            string username = "bossadmin12";
            string predicted = "1";

            string actual = _AS.UpdateRecoveryAttempts(username, DateTime.Now);

            Assert.True(predicted.Equals(actual));

            _AS.RemoveRecoveryAttempts(username);

        }

        [Fact]
        public void VerifySameDayTest()
        {
            string username = "bossadmin12";
            string code = _AS.GetRecoveryOTP(username);

            string predicted = "1";

            Assert.Equal(predicted,_AS.VerifySameDay(username, code));
        }

        [Fact]
        public void ResetPasswordTest()
        {
            string newPassword = "testPassword!1";
            string username = "bossadmin12";
            string oldPassword = _AS.GetPassword(username);

            string predicted = "1";

            Assert.Equal(_AS.ResetPassword(newPassword, username), predicted);

            _AS.ResetPassword(oldPassword, username);


        }
    }
}