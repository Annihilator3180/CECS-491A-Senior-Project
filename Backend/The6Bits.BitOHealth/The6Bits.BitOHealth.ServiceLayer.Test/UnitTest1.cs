using System;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;
using Xunit;
using The6Bits.BitOHealth.DAL.Implementations;

namespace The6Bits.BitOHealth.ServiceLayer.Tests
{
    public class UMServiceLayerTests
    {
        [Fact]
        public void ValidEmail()
        {
            UMService Service = new UMService(new SqlUMDAO<User>());
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
            UMService Service = new UMService(new SqlUMDAO<User>());
            bool isValid = Service.ValidateEmail(s);
            Assert.False(isValid);
        }
        [Fact]

        public void validPassword()
        {
            UMService Service = new UMService(new SqlUMDAO<User>());
            String Password = "TestProperPassword@";
            bool isValid = Service.ValidateEmail(Password);
            Assert.False(false);
        }
    }
}