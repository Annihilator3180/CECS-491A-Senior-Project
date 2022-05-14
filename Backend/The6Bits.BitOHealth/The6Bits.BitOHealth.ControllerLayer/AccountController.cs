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
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    private IAuthenticationService authenticationService1;
    private AccountManager _AccountManager;
    private LogService logService;
    private IDBErrors _dbErrors;
    private ISMTPEmailService _EmailService;
    private IConfiguration _config;
    private IAuthenticationService _auth;
    private bool isValid;


    public AccountController(IRepositoryAuth<string> authdao, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors,
        ISMTPEmailService emailService, IConfiguration config, IHashDao hashDao)
    {
        _AccountManager = new AccountManager(authdao, authenticationService, dbErrors, emailService, config, hashDao, config.GetSection("jwt").Value);
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






        var jwt = _AccountManager.Login(acc);
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
        /*
        String token = "";
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
        */
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
    public string SendOTP(string username, string password)
    {

        var otp = _AccountManager.SendOTP(username,password);
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
        string token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
        string username = _auth.getUsername(token);
        string status = _AccountManager.DeleteAccount(token);

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
        
        string creationStatus = _AccountManager.CreateAccount(user);
        if (creationStatus.Contains("Database"))
        {
            _= logService.Log(user.Username, "Registration- " + creationStatus, "Data Store", "Error");
            return "Database Error";
        }
        else if (creationStatus == "Email Failed To Send")
        {
            _ = logService.Log(user.Username, "Registration- Email Failed To Send", "Business", "Error");
            return "Email Failed To Send";
        }
        else if (creationStatus != "Email Pending Confirmation")
        {
            _ = logService.Log(user.Username, "Registration- " + creationStatus, "Business", "Information");
        }
        else
        {
            _ = logService.RegistrationLog(user.Username, "Verfication Email Sent", "Business", "Information");
        }
        return creationStatus;
    }

    [HttpPost("VerifyAccount")]
    public verifyResponse VerifyAccount(String Code, String Username)
    {

        verifyResponse verfied = _AccountManager.VerifyAccount(Code, Username);
        if (verfied.ErrorMessage.Contains("Database"))
        {
            _ = logService.Log(Username, "Registration- " + verfied, "Data Store", "Error");
            verfied.ErrorMessage = "Database Error";
            return verfied;
        }
        if (verfied.data == "Account Verified")
        {
            _ = logService.Log(Username, "Registration- Email Verified ", "Business", "Information");
            return verfied;
        }
        _ = logService.Log(Username, "Registration- " + verfied.ErrorMessage, "Business", "Information");
        return verfied;
    }

    public string AcceptEULA(string username)
    {
        bool isValid = authenticationService1.ValidateToken(Request.Headers["Authorization"]);
        username = authenticationService1.getUsername(Request.Headers["Authorization"]);

        if (isValid)
        {
            return _AccountManager.AcceptEULA(username);
        }
        return "invalid token";
    }

    public string DeclineEULA(string username)
    {
        bool isValid = authenticationService1.ValidateToken(Request.Headers["Authorization"]);
        username = authenticationService1.getUsername(Request.Headers["Authorization"]);

        if (isValid)
        {
            return _AccountManager.DeclineEULA(username);
        }
        return "invalid token";
    }


    [HttpPost("Recovery")]
    [Consumes("application/json")]

    public ActionResult AccountRecovery(AccountRecoveryModel arm) {
        AccountResponseModel response = new AccountResponseModel();

        string? token;

        token = Request.Headers["Authorization"];
        if (token != null)
        {
            response.ErrorMessage = "must be logged out";
            return BadRequest(response);
        }

        string start = _AccountManager.RecoverAccount(arm);

        if (start.Contains("Database"))
        {
            _ = logService.Log(arm.Username, " Account Recovery ", start, "Error");
            response.ErrorMessage = start;
            return StatusCode(500, response);
        }
        _ = logService.Log(arm.Username, " Account Recovery ", "Recovery Email Sent", "Information");

        response.Data = start;
        return Ok(response);


    }
    [HttpPost("ResetPassword")]
    public ActionResult ResetPassword(string randomString, string username, string password)
    {
        AccountResponseModel response = new AccountResponseModel();

        string? token;
        token = Request.Headers["Authorization"];
        if (token != null)
        {
            response.ErrorMessage = "Account Recovery Error";
            return BadRequest(response);
        }
        string reset = _AccountManager.ResetPassword(username, randomString, password);

        if (reset.Contains("Database"))
        {
            _ = logService.Log(username, " Password Reset ", reset, "Error");
            response.ErrorMessage = reset;
            return StatusCode(500, response);
        }
        _ = logService.Log(username, " Password Reset ", "Password Change ", "Information");

        response.Data= reset;
        return Ok(response);
    }

    [HttpPost("ViewTime")]
    public string ViewTime(float time, string view)
    {
        string updated = _AccountManager.ViewTime(time, view);
        _ = logService.Log("admin", updated, "Business", "Information");

        return updated;
    }
    [HttpPost("getTotalTime")]
    public async Task<List<timeTotal>> GetTotalTime()
    {
        try
        {
            List<timeTotal> total = await _AccountManager.GetTotalTime();
            _ = logService.Log("admin", "viewed totalTime", "Business", "Information");
            return total;
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost("getAvgTime")]
    public async Task<List<timeTotal>> GetAvgTime()
    {
        try
        {
            List<timeTotal> average = await _AccountManager.getAvgTime();
            _ = logService.Log("admin", "viewed average time", "Business", "Information");
            return average;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost("getSearchCount")]
    public List<searchItem> getSearchCount(string type)
    {
        try
        {
            List<searchItem> updated = _AccountManager.getSearchCount(type);
            _ = logService.Log("admin", "viewed search count for " +type, "Business", "Information");
            return updated;
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost("timeTracker")]
    public List<Tracking> timeTracker(string Type, int months)
    {
        try
        {
            List<Tracking> updated = _AccountManager.loginTracker(Type, months);
            _ = logService.Log("admin", "viewed" +Type+" total in "+months +"month", "Business", "Information");
            return updated;
        }
        catch (Exception)
        {
            throw;
        }
    }
    [HttpPost("regTracker")]
    public List<Tracking> regTracker()
    {
        try
        {
            List<Tracking> updated = _AccountManager.regTracker();
            _ = logService.Log("admin", "viewed registration total", "Business", "Information");
            return updated;
        }
        catch (Exception)
        {
            throw;
        }
    }

}



