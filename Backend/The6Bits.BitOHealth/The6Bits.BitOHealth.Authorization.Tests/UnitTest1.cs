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
            string encryptedString = encryptionMethods.encrypt(expected);
            string desired = encryptionMethods.decrypt(encryptedString);
           Assert.Equal(expected, desired);
        }

      
    }
}