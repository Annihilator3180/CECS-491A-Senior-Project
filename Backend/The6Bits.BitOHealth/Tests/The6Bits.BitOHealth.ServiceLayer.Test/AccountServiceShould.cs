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
        public void ValidEmailTest()
        {
            //arrange
            String Email = "testemail@email.com";
            //act
            bool isValid = _AS.ValidateEmail(Email);
            //assert
            Assert.True(isValid);
        }
        [Fact]

        public void InvalidEmailTest()
        {
            //arrange
            string Email = "Testemail";
            //act
            bool isValid = _AS.ValidateEmail(Email);
            //assert
            Assert.False(isValid);
        }
        [Fact]
        public void ValidPasswordTest()
        {
            //arrange
            string Password = "TestProperPassword@1";
            //act
            bool isValid = _AS.ValidateEmail(Password);
            //assert
            Assert.True(isValid);
        }
        [Fact]
        public void ShortPasswordTest()
        {
            //arrange
            string password = "Short@1";
            //act
            bool isValid = _AS.ValidatePassword(password);
            //assert
            Assert.False(isValid);
        }
        [Fact]
        public void InvalidSpecialPasswordTest()
        {
            //arrange
            string password = "BadSpecialCharacter$$$@1";
            //act
            bool isValid = _AS.ValidatePassword(password);
            //assert
            Assert.False(isValid);
        }
        [Fact]
        public void LongPasswordTest()
        {
            //arrange
            string password = "LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0NG@23@1";
            //act
            bool isValid = _AS.ValidatePassword(password);
            //assert
            Assert.False(isValid);
        }
        [Fact]
        public void LowerCasePasswordTest()
        {
            //arrange
            string password = "nouppercase@1";
            //act
            bool isValid = _AS.ValidatePassword(password);
            //assert
            Assert.False(isValid);
        }
        [Fact]
        public void LettersOnlyPasswordTest()
        {
            //arrange
            string password = "AllLettersonly";
            //act
            bool isValid = _AS.ValidatePassword(password);
            //assert
            Assert.False(isValid);
        }

        [Fact]
        public void NoNumbersPasswordTest()
        {
            //arrange
            string password = "NONUMBERSSSSSSSSSSSSSSSSSS@@";
            //act
            bool isValid = _AS.ValidatePassword(password);
            //assert
            Assert.False(isValid);
        }

        [Fact]
        public void NoSpecialPasswordTest()
        {
            //arrange
            string password = "NOsPECIAL1234@1";
            //act
            bool isValid = _AS.ValidatePassword(password);
            //assert
            Assert.False(isValid);
        }

        [Fact]
        public void ValidUsername()
        {
            //arrange
           string UserName = "UsernameGood@1";
            //act
            string returnString = _AS.ValidateUsername(UserName);
            //assert
           Assert.Equal("new username", returnString);
        }


        [Theory]
        [InlineData("Short1")]
        [InlineData("$$$$$$$$$$$$$$$$$$$$$")]
        [InlineData("LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0NG@23")]
        public void InvalidUsername(string Username)
        {
            string returnString = _AS.ValidateUsername(Username);
            Assert.Equal("Invalid Username", returnString);
        }
        [Fact]
        public void UsernameAndEmailTest()
        {
            //arrange
            string email = "cbass@gmail.com";
            string username = "bossadmin12";
            string predicted = "Email and Username found";
            //act
            string actual = _AS.UsernameAndEmailExists(username, email);
            //assert
            Assert.Equal(actual, predicted);
        }

        [Fact]
        public void IsEnabledTest()
        {
            //arrange
            string username = "bossadmin12";
            string predicted = "enabled";
            //act
            string actual = _AS.IsEnabled(username);
            //assert
            Assert.Equal(predicted, actual);
        }
        [Fact]
        public void ValidateRecoveryAttemptsTest()
        {
            //arrange
            string username = "bossadmin12";
            string predicted = "under";
            //act
            string actual = _AS.ValidateRecoveryAttempts(username);
            //assert
            Assert.Equal(actual, predicted);
        }
        [Fact]
        public void UpdateRecoveryAttemptsTest()
        {
            //arrange
            string username = "bossadmin12";
            string predicted = "1";
            //act
            string actual = _AS.UpdateRecoveryAttempts(username, DateTime.Now);
            //assert
            Assert.True(predicted.Equals(actual));
            //cleanup
            _AS.RemoveRecoveryAttempts(username);

        }

        [Fact]
        public void VerifySameDayTest()
        {
            //arrange
            string username = "bossadmin12";
            string predicted = "1";
            //act
            string code = _AS.GetRecoveryOTP(username);

           
            //assert
            Assert.Equal(predicted,_AS.VerifySameDay(username, code));
        }

        [Fact]
        public void ResetPasswordTest()
        {
            //arrange
            string newPassword = "testPassword!1";
            string predicted = "1";
            string username = "bossadmin12";
            //act
            string oldPassword = _AS.GetPassword(username);


            //assert

            Assert.Equal(_AS.ResetPassword(newPassword, username), predicted);
            
            //cleanup
            _AS.ResetPassword(oldPassword, username);


        }
    }
}