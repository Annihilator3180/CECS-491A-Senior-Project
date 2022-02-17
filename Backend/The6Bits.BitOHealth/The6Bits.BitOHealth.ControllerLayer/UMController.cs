using System;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Authorization.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Http;


namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("[controller]")]
    public class UMController : ControllerBase
    {
        private UMManager _UMM;
        private LogService logService;
        private string adminUsername;

        public UMController(IRepositoryUM<User> daoType , IAuthorizationService auth ,ILogDal logDao)
        {
            _UMM = new UMManager(daoType);
            _UMM.auth = auth;
            adminUsername = "buhss";
            logService = new LogService(logDao);
        }

        [HttpGet]
        public string Index()
        {
            return "This is my default action...";
        }

        [HttpPost("Create")]
        [Consumes("application/json")]
        //specify form body
        public string CreateAccount(User u)
        {


            string res = _UMM.CreateAccount(u);

            if (res == "username exists")
            {
                logService.Log(adminUsername, "Account creation-Username Exists", "Info", "Business");
            }
            else if (res == "database error")
            {
                logService.Log(adminUsername, "Create Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Account Creation -"+res, "Info", "Business");
            }
            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddHours(1),
                IsEssential = true,
                HttpOnly = false,
                Secure = false,
            };
            Response.Cookies.Append("MyCookie", "TheValue", cookieOptions);
            return res;
        }
        public string DeleteAccount(string username)
        {
            string res = _UMM.DeleteAccount(username);

            if (res != "username exists")
            {
                logService.Log(adminUsername, "Delete Account- new username", "Info", "Business");
            }
            else if (res == "database error")
            {
                logService.Log(adminUsername, "Delete Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Delete Account -"+res, "Info", "Business");
            }

            return res;
        }
        public string UpdateAccount(User user)
        {
            string res = _UMM.UpdateAccount(user);

            if (res != "username exists")
            {
                logService.Log(adminUsername, "Update Account- new username", "Info", "Business");
            }
            else if (res == "database error")
            {
                logService.Log(adminUsername, "Update Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Update Account -" + res, "Info", "Business");
            }
            return res;
        }
        public string EnableAccount(string username)
        {
            string res = _UMM.EnableAccount(username);
            if (res != "username exists")
            {
                logService.Log(adminUsername, "Enabel Account - new username", "Info", "Business");
            }
            else if (res == "database error")
            {
                logService.Log(adminUsername, "Enable Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Enable Account -" + res, "Info", "Business");
            }
            return res;
        }
        public string DisableAccount(string username)
        {
            string res = _UMM.DisableAccount(username);
            if (res != "username exists")
            {
                logService.Log(adminUsername, "Disable Account- new username", "Info", "Business");
            }
            else if (res == "database error")
             {
                logService.Log(adminUsername, "Disable Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Disable Account -" + res, "Info", "Business");
            }
            return res;
        }


    }
}