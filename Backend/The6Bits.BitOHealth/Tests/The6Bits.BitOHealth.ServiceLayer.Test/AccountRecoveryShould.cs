using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.EmailService;
using Xunit;

namespace The6Bits.BitOHealth.AccountRecovery.Test
{
    public class AccountRecoveryShould
    {
        AccountService _AS;

        public AccountRecoveryShould()
        {
            _AS = new AccountService(new AccountMsSqlDao("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;"));
        }
        [Fact]
        public void EmailAndUsernameExist()
        {
            string email = "cbass@gmail.com";
            string username = "bossadmin12";

            string expected = "Email and Username found";

            Assert.Equal(expected, _AS.UsernameAndEmailExists(username, email));

        }
        [Fact]
        public void IsEnabled()
        {
            string username = "bossadmin12";

            string expected = "enabled";

            Assert.Equal(expected, _AS.IsEnabled(username));
        }
        [Fact]
        public void ValidateRecoveryAttempts()
        {
            string username = "bossadmin12";

            string expected = "under";

            Assert.Equal(expected, _AS.ValidateRecoveryAttempts(username));

        }
    



    }
}
