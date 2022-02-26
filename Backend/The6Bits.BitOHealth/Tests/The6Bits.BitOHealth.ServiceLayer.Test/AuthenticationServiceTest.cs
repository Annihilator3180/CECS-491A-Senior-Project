using System;
using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using Xunit;

namespace The6Bits.BitOHealth.ServiceLayer.Tests
{
    public class AuthenticationServiceTest
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationServiceTest()
        {
            _authService = new JWTAuthenticationService();
        }

        [Theory]
        [InlineData("test.test.test")]
        public void InvalidToken(string token)
        {
            var isValid = _authService.ValidateToken(token);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw")]
        public void ValidToken(string token)
        {
            var isValid = _authService.ValidateToken(token);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw")]
        public void GetUserNameShould(string token)
        {
            var user = "firstuser29";
            var userName = _authService.getUsername(token);

            Assert.Equal(userName, user);
        }

        [Theory]
        [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw")]
        public void GetUserNameShouldNot(string token)
        {
            var user = "ddfscsa29";
            var userName = _authService.getUsername(token);

            Assert.NotEqual(userName, user);
        }
    }
}