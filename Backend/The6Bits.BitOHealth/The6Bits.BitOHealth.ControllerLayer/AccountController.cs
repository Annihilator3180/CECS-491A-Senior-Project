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
using The6Bits.EmailService;
// using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    private IAuthenticationService authenticationService1;
    private AccountManager _AM;
    private LogService logService;
    private IDBErrors _dbErrors;
    private ISMTPEmailServiceShould _EmailService;
    public AccountController(IRepositoryAuth<string> authdao ,ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors, ISMTPEmailServiceShould EmailService)
    {
        _AM = new AccountManager(authdao,authenticationService,dbErrors,EmailService);
        logService = new LogService(logDao);
        _dbErrors = dbErrors;
        _EmailService = EmailService;
        authenticationService1 = authenticationService;
    }

    [HttpPost("Login")]
    public string Login(LoginModel acc)
    {
        var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();



        var jwt =  _AM.Login(acc);
        var parts = jwt.Split('.');
        
        //TODO:FIX IF STATMENT TO SOMETHING BETTER
        //TODO:ADD LOGS
        if (parts.Length==3)
        {
            Response.Cookies.Append(
                "token",
                jwt);
            logService.LoginLog(acc.Username + remoteIpAddress, "Logged In", "Info","Business" );
        }
        else
        {
            string loginfail = "Log In Fail";
            if (jwt.Contains("Database"))
            {
                logService.LoginLog(acc.Username + remoteIpAddress, loginfail+" "+jwt, "Error","Data Store" );
            }
            else
            {
                logService.LoginLog(acc.Username + remoteIpAddress, loginfail+" "+jwt, "Info","Business" );
            }

        }

        return jwt;

    }
    
    [HttpPost("OTP")]
    public string SendOTP(string username)
    {
        
        var otp =  _AM.SendOTP(username);
        if (otp.Contains("Database"))
        {
            logService.Log(username, otp , "OTP Error " + "Error", "Data Store");
        }
        else
        {
            logService.Log(username, "OTP " + otp, "Info", "Business");
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
        var token = "";
        if(Request.Headers.ContainsKey("Authorization"))
        {
            token = Request.Headers["Authorization"].ToString();
            if(token.StartsWith("Bearer "))
            {
                token = token.Remove(0, 7);
            }

        }
        string username = authenticationService1.getUsername(token);
        string status =  _AM.DeleteAccount(token);

        if (status.Contains("Database"))
        {
            logService.Log(username, "Account Deletion- " + status, "Data Store ", "Error");
        }
        else
        {
            logService.Log(username, "Account Deletion- " + status, "Business", "Information");
        }

        Response.Cookies.Delete(token);
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

        String CreationStatus = _AM.CreateAccount(user);
        if (CreationStatus.Contains("Database"))
        {
            logService.RegistrationLog(user.Username, "Registration- " + CreationStatus, "Data Store", "Error");
            return "Database Error";
        }
        else if (CreationStatus == "Email Failed To Send")
        {
            logService.RegistrationLog(user.Username, "Registration- Email Failed To Send", "Business", "Error");
            return "Email Failed To Send";
        }
        else if (CreationStatus != "Email Pending Confirmation") {
            logService.RegistrationLog(user.Username, "Registration- "+CreationStatus, "Business", "Information");
                }
        else
        {
            logService.RegistrationLog(user.Username, "Verfication Email Sent", "Business", "Information");
        }
        return CreationStatus;
    }

    [HttpGet("VerifyAccount")]
    public string VerifyAccount(String Code, String Username)
    {
        String verfied = _AM.VerifyAccount(Code, Username);
        if (verfied.Contains("Database"))
        {
            logService.Log(Username, "Registration- " + verfied, "Data Store", "Error");
            return "Database Error";
        }
        if(verfied == "Account Verified")
        {
            logService.Log(Username, "Registration- Email Verified ", "Business", "Information");
            return verfied;
        }
        logService.Log(Username, "Registration- Email Verified ", "Data Store", "Verified");
        return verfied;
    }

    public string AcceptEULA(string username)
    {
        bool isValid = authenticationService1.ValidateToken(Request.Headers["Authorization"]);
        username = authenticationService1.getUsername(Request.Headers["Authorization"]);

        if (isValid)
        {
            // if usernameExists(username) { return _AM.AcceptEULA(username) } return "invalid username";
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
            //check username
            return _AM.DeclineEULA(username);
        }
        return "invalid token";
    }


    [HttpPost("Recovery")]
    [Consumes("application/json")]

    public string AccountRecovery(AccountRecoveryModel arm)
    {

        return _AM.recoverAccount(arm);


    }
    [HttpPost("ResetPassword")]
    public string ResetPassword(string r, string u, string p)
    {
        return _AM.ResetPassword(u, r, p);
    }

}



