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
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL.Contract;


namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("UM")]
    public class UMController : ControllerBase
    {
        private UMManager _UMM;
        private LogService logService;
        private string adminUsername;
        private bool isValid;
        private IAuthenticationService _authentication;
        

        
        public UMController(IRepositoryUM<User> daoType , IAuthenticationService authentication ,ILogDal logDao)
        {
            _UMM = new UMManager(daoType);
            _authentication = authentication;
            adminUsername = "buhss";
            logService = new LogService(logDao);
        }

        [HttpGet]
        public string Index()
        {
            return "This is my default action...";
        }

        [HttpPost("CreateAccount")]
        //specify form body
        public string CreateAccount(User u)
        {
            isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);
            string adminUsername = _authentication.getUsername(Request.Headers["Authorization"]);
            
            if (isValid)
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

                return res;
            }


                logService.Log("None", "Account Creation -"+"InvalidToken", "Info", "Business");
            
            
            return "InvalidToken";
        }
        
        [HttpPost("DeleteAccount")]
        public string DeleteAccount(string username)
        {
            isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);
            string adminUsername = _authentication.getUsername(Request.Headers["Authorization"]);
            if (isValid)
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
                    logService.Log(adminUsername, "Delete Account -" + res, "Info", "Business");
                }
                return res;

            }
            
            logService.Log("None", "DeleteAccount -"+"InvalidToken", "Info", "Business");
            
            
            return "InvalidToken";
        }
        
        [HttpPost("UpdateAccount")]

        public string UpdateAccount(User user)
        {
            isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);
            string adminUsername = _authentication.getUsername(Request.Headers["Authorization"]);
            if (isValid)
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
            logService.Log("None", "UpdateAccount -"+"InvalidToken", "Info", "Business");
            
            
            return "InvalidToken";
        }
        [HttpPost("EnableAccount")]
        public string EnableAccount(string username)
        {
            isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);
            string adminUsername = _authentication.getUsername(Request.Headers["Authorization"]);
            if (isValid)
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
            logService.Log("None", "EnableAccount -"+"InvalidToken", "Info", "Business");
            
            
            return "InvalidToken";
        }
        
        
        [HttpPost("DisableAccount")]

        public string DisableAccount(string username)
        {
            isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);
            string adminUsername = _authentication.getUsername(Request.Headers["Authorization"]);
            if (isValid)
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

            logService.Log("None", "DisableAccount -"+"InvalidToken", "Info", "Business");
            
            
            return "InvalidToken";
        }




    }
}