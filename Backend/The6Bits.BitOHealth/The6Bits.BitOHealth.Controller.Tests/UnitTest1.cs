using Xunit;
using The6Bits.Authorization;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.ControllerLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.Logging.DAL.Contracts;
using System.Net.Http;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.DBErrors;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging.DAL.Implementations;

namespace The6Bits.Authorization.Tests
{
    public class DESAuthorizationShould : ControllerBase
    {
        

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
            MedicationController controller = new MedicationController(new MsSqlMedicationDAO(conn), new OpenFDADAO(new HttpClient(), new openFDAConfig()), new SQLLogDAO(),
            _authenticationService, new MsSqlDerrorService(), new ReminderMsSqlDao(conn));
            
            DefaultHttpContext context = new DefaultHttpContext();
            //context.Request.Cookies.Append("token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw",
            //  option);
            context.Request.Headers.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImZpcnN0dXNlcjI5IiwiaWF0IjoiMTY0NTg2ODc4NCJ9.D6CaUg_WOE4UxSPljdxb90B3bvCnm9u8IFL9dVM-viw");
            //controller.ControllerContext.HttpContext = context;
            string s = controller.AddFavorites("generic name", "brand name", "123");
            Assert.Equal("progress", s);


        }
    }
}