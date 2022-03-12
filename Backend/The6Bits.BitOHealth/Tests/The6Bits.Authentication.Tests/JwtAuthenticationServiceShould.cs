using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using Xunit;
using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.DAL.Tests;


namespace The6Bits.Authentication.Tests
{
    public class JwtAuthenticationServiceShould : TestsBase
    {
        private IAuthenticationService _authenticationService;
        public JwtAuthenticationServiceShould()
        {
            _authenticationService = new JWTAuthenticationService(keyPath);
        }


        [Theory]
        [InlineData("zz")]
        [InlineData("dasdsadassddasdsa")]
        public void GenerateToken(string data)
        {
            string token = _authenticationService.generateToken(data, new ClaimsIdentity());

            int siz = token.Split('.').Length;
            Assert.Equal(3, siz);

        }


        [Theory]
        [InlineData("test.test.test")]
        public void InvalidToken(string token)
        {
            var isValid = _authenticationService.ValidateToken(token);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJc0FkbWluIjoiMSIsIklzRW5hYmxlZCI6IjEiLCJwcml2T3B0aW9uIjoiMSIsInVzZXJuYW1lIjoiYm9zc2FkbWluMTIiLCJpYXQiOiIxNjQ2OTY3NjAzIn0=.Vp6eRgMCFat//9KY3rxk/Pa/EvVmmARUTrX69EzJ0PA=")]
        public void ValidToken(string token)
        {
            var isValid = _authenticationService.ValidateToken(token);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw")]
        public void GetUserNameShould(string token)
        {
            var user = "firstuser29";
            var userName = _authenticationService.getUsername(token);

            Assert.Equal(userName, user);
        }

        [Theory]
        [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw")]
        public void GetUserNameShouldNot(string token)
        {
            var user = "ddfscsa29";
            var userName = _authenticationService.getUsername(token);

            Assert.NotEqual(userName, user);
        }
    }
}