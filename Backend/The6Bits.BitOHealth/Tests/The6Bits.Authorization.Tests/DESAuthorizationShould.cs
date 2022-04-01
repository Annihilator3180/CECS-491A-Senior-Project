using Xunit;
using The6Bits.Authorization;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.ControllerLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using The6Bits.Authentication.Implementations;

namespace The6Bits.Authorization.Tests
{
    public class DESAuthorizationShould  : ControllerBase
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
    

        [Fact]
        public void makeFindDrug()
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.Combine(AppContext.BaseDirectory, @"..\..\..\"))
                .AddJsonFile("appsettings.json")
                .Build();
            string conn = configuration.GetConnectionString("DefaultConnection");
            string keyPath = configuration.GetSection("PKs")["JWT"];
            // Do "global" initialization here; Called before every test method.
            /**MedicationManager _MM;
            LogService logService;
            IDBErrors _dbErrors;
            IConfiguration _config;
            IAuthenticationService _auth;**/
            JWTAuthenticationService _authenticationService = new JWTAuthenticationService(keyPath);
            MedicationController controller = new MedicationController(_authenticationService);
            DefaultHttpContext mycock = new DefaultHttpContext();
            //mycock.Request.Cookies.Append("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw",
            //  option);
            mycock.Request.Headers.Add("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw");
            controller.ControllerContext.HttpContext = mycock;
            string s = controller.FindDrug("adderall");
            Assert.Equal("progress",s);


        }
    }
}
