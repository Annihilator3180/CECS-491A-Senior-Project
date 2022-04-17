using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;
using The6Bits.DBErrors;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using The6Bits.EmailService;
using The6Bits.HashAndSaltService;
using The6Bits.HashAndSaltService.Contract;
using System.Data.SqlClient;
using Dapper;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    private IAuthenticationService authenticationService1;
    private AccountManager _AM;
    private LogService logService;
    private IDBErrors _dbErrors;
    private ISMTPEmailService _EmailService;
    private IConfiguration _config;
    private IAuthenticationService _auth;
    private bool isValid;


    public AccountController(IRepositoryAuth<string> authdao, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors,
        ISMTPEmailService emailService, IConfiguration config, IHashDao hashDao)
    {
        _AM = new AccountManager(authdao, authenticationService, dbErrors, emailService, config, hashDao, config.GetSection("jwt").Value);
        logService = new LogService(logDao);
        _dbErrors = dbErrors;
        _EmailService = emailService;
        _auth = authenticationService;
        _config = config;
    }

    [HttpPost("Login")]
    public string Login(LoginModel acc)
    {
        var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();


        //HASH
        //DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        //string p = di.Parent.ToString();
        //string mySecret = System.IO.File.ReadAllText(Path.GetFullPath(p + _config.GetSection("PKs")["JWT"]));
        //byte[] keyBytes = Encoding.UTF8.GetBytes(mySecret);
        //var bytesToSign = Encoding.UTF8.GetBytes(acc.Password);
        //var sha = new HMACSHA256(keyBytes);
        //byte[] signature = sha.ComputeHash(bytesToSign);
        //acc.Password = Convert.ToBase64String(signature);




        var jwt = _AM.Login(acc);
        var parts = jwt.Split('.');

        if (parts.Length == 3)
        {
            var cookieOptions = new CookieOptions()
            {
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(14),
                SameSite = SameSiteMode.None,
                HttpOnly = true,
            };
            Response.Cookies.Append(
                "token",
                jwt, cookieOptions);

            _ =logService.LoginLog(acc.Username + remoteIpAddress, "Logged In", "Info", "Business");
        }
        else
        {

            string loginfail = "Log In Fail";
            if (jwt.Contains("Database"))
            {
                _ = logService.LoginLog(acc.Username + remoteIpAddress, loginfail + " " + jwt, "Error", "Data Store");
            }
            else
            {
                _ = logService.LoginLog(acc.Username + remoteIpAddress, loginfail + " " + jwt, "Info", "Business");
            }

        }

        return jwt;

    }

    [HttpPost("getTLogs")]
    public string getTrackerLogs()
    {
        
        string token = "";
        try
        {
            token = Request.Cookies["token"];
        }
        catch
        {
            return "No token";
        }
        isValid = authenticationService1.ValidateToken(token);
        if (!isValid)
        {
            _ = logService.Log("None", "Invalid Token - Get Tracker Logs", "Info", "Business");
            return "Invalid Token";
        }
        
        string s = logService.getAllTrackerLogs();
        string[] subs = s.Split(' ');
        string holder = "";
        int counter = 0;
        foreach (var sub in subs)
        {
            if(counter == 0)
            {
                holder = sub;
            }
            else if(counter != 0)
            {
                if (counter == 4)
                {
                    holder += "," + sub;
                    counter = 1;
                }
                else
                {
                    holder += " " + sub;
                }
            }
            counter++;
        }
        return holder;

    }

    [HttpPost("OTP")]
    public string SendOTP(string username)
    {

        var otp = _AM.SendOTP(username);
        if (otp.Contains("Database"))
        {
            _ = logService.Log(username, otp, "OTP Error " + "Error", "Data Store");
        }
        else
        {
            _ = logService.Log(username, "OTP " + otp, "Info", "Business");
        }
        return otp;
    }

    [HttpPost("Logout")]
    public string Logout()
    {
        Response.Cookies.Delete("token");
        return "Account logged out";
    }
    // JSON 
    [HttpPost("Delete")]
    public string DeleteAccount()
    {
        string token = Request.Cookies["token"];
        string username = _auth.getUsername(token);
        string status = _AM.DeleteAccount(token);

        if (status.Contains("Database"))
        {
            _ =logService.Log(username, "Account Deletion- " + status, "Data Store ", "Error");
        }
        else
        {
            _ = logService.Log(username, "Account Deletion- " + status, "Business", "Information");
        }

        Response.Cookies.Delete("token");
        return status;
    }



    //   public void deletecookie(object sender, eventargs e)
    //   {
    //       //httpcookie httpcookie = new httpcookie();
    //      httpcookie httpcookie = request.cookies.get("cookie");
    //      httpcookie.expires = datetime.now.adddays(-1d);
    //      response.cookies.append("cookie",httpcookie);
    //   }




    [HttpPost("Register")]
    public string CreateAccount(User user)
    {

        string creationStatus = _AM.CreateAccount(user);
        if (creationStatus.Contains("Database"))
        {
            _= logService.RegistrationLog(user.Username, "Registration- " + creationStatus, "Data Store", "Error");
            return "Database Error";
        }
        else if (creationStatus == "Email Failed To Send")
        {
            _ = logService.RegistrationLog(user.Username, "Registration- Email Failed To Send", "Business", "Error");
            return "Email Failed To Send";
        }
        else if (creationStatus != "Email Pending Confirmation")
        {
            _ = logService.RegistrationLog(user.Username, "Registration- " + creationStatus, "Business", "Information");
        }
        else
        {
            _ = logService.RegistrationLog(user.Username, "Verfication Email Sent", "Business", "Information");
        }
        return creationStatus;
    }

    [HttpGet("VerifyAccount")]
    public string VerifyAccount(String Code, String Username)
    {

        String verfied = _AM.VerifyAccount(Code, Username);
        if (verfied.Contains("Database"))
        {
            _ = logService.Log(Username, "Registration- " + verfied, "Data Store", "Error");
            return "Database Error";
        }
        if (verfied == "Account Verified")
        {
            _ = logService.Log(Username, "Registration- Email Verified ", "Business", "Information");
            return verfied;
        }
        _ = logService.Log(Username, "Registration- " + verfied, "Business", "Information");
        return verfied;
    }

    public string AcceptEULA(string username)
    {
        bool isValid = authenticationService1.ValidateToken(Request.Headers["Authorization"]);
        username = authenticationService1.getUsername(Request.Headers["Authorization"]);

        if (isValid)
        {
            return _AM.AcceptEULA(username);
        }
        return "invalid token";
    }

    public string DeclineEULA(string username)
    {
        bool isValid = authenticationService1.ValidateToken(Request.Headers["Authorization"]);
        username = authenticationService1.getUsername(Request.Headers["Authorization"]);

        if (isValid)
        {
            return _AM.DeclineEULA(username);
        }
        return "invalid token";
    }


    [HttpPost("Recovery")]
    [Consumes("application/json")]

    public string AccountRecovery(AccountRecoveryModel arm)
    {
        string start = _AM.RecoverAccount(arm);

        if (start.Contains("Database"))
        {
            _ = logService.Log(arm.Username, " Account Recovery ", start, "Error");
            return "Database Error";
        }
        _ = logService.Log(arm.Username, " Account Recovery ", "Recovery Email Sent", "Information");

        return start;


    }
    [HttpPost("ResetPassword")]
    public string ResetPassword(string randomString, string username, string password)
    {
        string reset = _AM.ResetPassword(username, randomString, password);

        if (reset.Contains("Database"))
        {
            _ = logService.Log(username, " Password Reset ", reset, "Error");
            return "Database Error";
        }
        _ = logService.Log(username, " Password Reset ", "Password Change ", "Information");

        return reset;
    }

}



