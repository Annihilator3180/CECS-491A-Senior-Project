﻿using System;
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
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.Authorization;
using The6Bits.HashAndSaltService.Contract;

namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("UM")]
    public class UMController : ControllerBase
    {
        private UMManager _UMM;
        private LogService logService;
        private bool isValid;
        private IAuthenticationService _authentication;

        private AuthorizationService _authorization;


        public UMController(IRepositoryUM<User> daoType , IAuthenticationService authentication ,ILogDal logDao, IAuthorizationDao authorizationDao, IHashDao hashDao, IConfiguration config)
        {
            _UMM = new UMManager(daoType,authorizationDao, hashDao, config);
            _authentication = authentication;
            logService = new LogService(logDao);
            _authorization= new AuthorizationService(authorizationDao);
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


            if (isValid)
            {

                string token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];

                string adminUsername = _authentication.getUsername(token);
                if (!_authorization.VerifyClaim(token, "IsAdmin"))
                {
                    logService.Log(adminUsername, "Account creation-Claims Denied", "Info", "Business");
                    return "InvalidClaims";
                }
                
                
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
            
            
            return Request.Cookies["token"];
        }
        
        [HttpPost("DeleteAccount")]
        public string DeleteAccount(string username)
        {
            isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);


            if (isValid)
            {

                string token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];

                string adminUsername = _authentication.getUsername(token);
                if (!_authorization.VerifyClaim(token, "IsAdmin"))
                {
                    logService.Log(adminUsername, "Account Delete-Claims Denied", "Info", "Business");
                    return "InvalidClaims";
                }

                if (username == "bossadmin12")
                {
                    return "Cannot delete user.";
                }


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


            if (isValid)
            {

                string token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];

                string adminUsername = _authentication.getUsername(token);
                if (!_authorization.VerifyClaim(token, "IsAdmin"))
                {
                    logService.Log(adminUsername, "Account Update-Claims Denied", "Info", "Business");
                    return "InvalidClaims";
                }
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


            if (isValid)
            {

                string token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];
                string adminUsername = _authentication.getUsername(token);
                if (!_authorization.VerifyClaim(token, "IsAdmin"))
                {
                    logService.Log(adminUsername, "Account Update-Claims Denied", "Info", "Business");
                    return "InvalidClaims";
                }
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


            if (isValid)
            {

                string token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];

                string adminUsername = _authentication.getUsername(token);
                if (!_authorization.VerifyClaim(token, "IsAdmin"))
                {
                    logService.Log(adminUsername, "Account Disable-Claims Denied", "Info", "Business");
                    return "InvalidClaims";
                }
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
        [HttpPost("UploadFile")]
        public async Task<string> UploadFile(IFormFile file)
        {
            isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);


            if (!isValid) return "Invalid Token";


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();



            string token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];


            string adminUsername = _authentication.getUsername(token);

            if (!_authorization.VerifyClaim(token, "IsAdmin"))
            {
                logService.Log(adminUsername, "BULK -Claims Denied", "Info", "Business");
                return "InvalidClaims";
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    string? line = await reader.ReadLineAsync();
                    if (line == null)
                    {
                        continue;
                    }
                    var arr = line.Split(' ');
                    if (line.Contains("create"))
                    {
                        _UMM.CreateAccount(new User(arr[1], arr[2], arr[3], arr[4], arr[5], Int32.Parse(arr[6]), Int32.Parse(arr[7]), Int32.Parse(arr[8])));
                    }

                    if (line.Contains("update"))
                    {
                        _UMM.UpdateAccount(new User(arr[1], arr[2], arr[3], arr[4], arr[5], Int32.Parse(arr[6]), Int32.Parse(arr[7]), Int32.Parse(arr[8])));

                    }

                    if (line.Contains("enable"))
                    {
                        _UMM.EnableAccount(arr[1]);
                    }

                    if (line.Contains("disable"))
                    {
                        _UMM.DisableAccount(arr[1]);
                    }

                }

            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            return elapsedTime;

        }



    }
}