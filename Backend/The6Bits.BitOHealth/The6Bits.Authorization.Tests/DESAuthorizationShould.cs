using Xunit;
using The6Bits.Authorization;
using The6Bits.Authorization.Implementations;


namespace The6Bits.Authorization.Tests
{
    public class DESAuthorizationShould
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