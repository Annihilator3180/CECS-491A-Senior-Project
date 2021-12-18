using Xunit;
using The6Bits.Authorization;
using The6Bits.Authorization.Implementations;


namespace The6Bits.Authorization.Tests
{
    public class DESAuthorizationShould
    {
        [Fact]
        public void encryptThenDecrypt()
        {
            DESAuthorizationService service = new DESAuthorizationService();
            string expected = "test string";
            string encryptedString = service.generateToken(expected);
            string desired = service.Decrypt(encryptedString);
            Assert.Equal(expected, desired);
        }
        [Fact]
        public void veriflyClaims()
        {
            DESAuthorizationService service = new DESAuthorizationService();
            string access = "Admin";
            string data = "this is a test" + access;
            string encryptedString = service.generateToken(data);
            Assert.True(service.VerifyClaims(encryptedString, access));
        }
    }
}