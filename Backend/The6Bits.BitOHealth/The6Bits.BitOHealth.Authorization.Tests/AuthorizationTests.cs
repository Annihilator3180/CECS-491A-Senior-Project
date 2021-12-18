using System;
using Xunit;

namespace The6Bits.BitOHealth.Authorization.Tests
{
    public class encryptionTest
    {
        [Fact]
        public void Test1()
        {
            string expected = "test string";
            string encryptedString = encryptionMethods.generateToken(expected);
            string desired = encryptionMethods.verifyClaims(encryptedString);
            Assert.Equal(expected, desired);
        }

      
    }
}