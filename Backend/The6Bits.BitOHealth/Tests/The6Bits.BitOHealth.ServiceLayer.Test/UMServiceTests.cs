using System;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;
using Xunit;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Contract;
using Moq;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.ManagerLayer;

namespace The6Bits.BitOHealth.ServiceLayer.Tests
{
    public class UMServiceLayerTests
    {
        [Fact]
        public void ValidEmail()
        {
            UMService Service = new UMService(new MsSqlUMDAO<User>());
            String Email = "testemail@email.com";
            bool isValid = Service.ValidateEmail(Email);
            Assert.True(isValid);
        }
        [Theory]
        [InlineData("testemail")]
        [InlineData("testemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemailtestemail")]
        [InlineData("testemail@")]
        public void InvalidEmail(string s)
        {
            UMService Service = new UMService(new MsSqlUMDAO<User>());
            bool isValid = Service.ValidateEmail(s);
            Assert.False(isValid);
        }
        [Fact]

        public void ValidPassword()
        {
            UMService Service = new UMService(new MsSqlUMDAO<User>());
            string Password = "TestProperPassword@1";
            bool isValid = Service.ValidateEmail(Password);
            Assert.True(isValid);
        }
        [Theory]
        [InlineData("Short@1")]
        [InlineData("LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0NG@23")]
        [InlineData("nouppercase@1")]
        [InlineData("NOLOWERCASE@1")]
        [InlineData("NOsPECIAL1234")]
        [InlineData("NONUMBERSSSSSSSSSSSSSSSSSS@@")]
        [InlineData("AllLettersonly")]
        [InlineData("BadSpecialCharacter$$$@1")]
        [InlineData("*********************")]
        public void InvalidPassword(string password)
        {
            UMService Service = new UMService(new MsSqlUMDAO<User>());
            bool isValid = Service.ValidatePassword(password);
            Assert.False(isValid);
        }


        //[Fact]
        //public void ValidUsername()
        //{
        //    UMService Service = new UMService(new MsSqlUMDAO<User>());
        //    string UserName = "UsernameGood@1";
        //    string returnString = Service.ValidateUsername(UserName);
        //    Assert.Equal("new username", returnString);
        //}


        [Theory]
        [InlineData("Short1")]
        [InlineData("$$$$$$$$$$$$$$$$$$$$$")]
        [InlineData("LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO0NG@23")]
        public void InvalidUsername(string Username)
        {
            UMService Service = new UMService(new MsSqlUMDAO<User>());
            string returnString = Service.ValidateUsername(Username);
            Assert.Equal("Invalid Username", returnString);
        }

        //[Fact]
        //public void DeleteAccountShouldSuccessfull()
        //{
        //    var repoAuth = new Mock<IRepositoryAuth<string>>();
        //    var authService = new Mock<IAuthenticationService>();
        //    var accountService = new Mock<IAccountService>();

        //    var accountManager = new AccountManager(repoAuth.Object, authService.Object, accountService.Object);

        //    authService.Setup(_ => _.ValidateToken(It.IsAny<string>())).Returns(true);

        //    authService.Setup(_ => _.getUsername(It.IsAny<string>())).Returns("test username");

        //    accountService.Setup(_ => _.UsernameExists(It.IsAny<string>())).Returns("username exists");

        //    accountService.Setup(_ => _.DeleteAccount(It.IsAny<string>())).Returns("Account Deleted");

        //    var result = accountManager.DeleteAccount("");

        //    Assert.True(result == "Account Deleted");
        //}
    }
}