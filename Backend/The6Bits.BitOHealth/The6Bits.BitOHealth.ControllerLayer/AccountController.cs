using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using Microsoft.Extensions.Configuration;
// using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    private AccountManager _AM;
    private LogService logService;
    private IDBErrors _dbErrors;
    private ISMTPEmailService _EmailService;
    private IConfiguration _config;
    public AccountController(IRepositoryAuth<string> authdao, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors, ISMTPEmailService emailService, IConfiguration config)
    {
        _AM = new AccountManager(authdao, authenticationService, dbErrors, emailService, config);
        logService = new LogService(logDao);
        _dbErrors = dbErrors;
        _EmailService = emailService;
        authenticationService1 = authenticationService;
        _config = config;
    }

    [HttpPost("Login")]
    public string Login(LoginModel acc)
    {


        var jwt = _AM.Login(acc);
        var parts = jwt.Split('.');

        //TODO:FIX IF STATMENT TO SOMETHING BETTER
        //TODO:ADD LOGS
        if (parts.Length == 3)
        {
            Response.Cookies.Append(
                "token",
                jwt);
            logService.Log(acc.Username, "Logged In", "Info", "Business");
        }
        else
        {
            string loginfail = "Log In Fail";
            logService.Log(acc.Username, loginfail + " " + jwt, "Info", "Business");

        }

        return jwt;

    }

    [HttpPost("OTP")]
    public string SendOTP(string username)
    {

        var otp = _AM.SendOTP(username);
        return otp;
    }

    [HttpPost("Logout")]
    public string Logout()
    {
        Response.Cookies.Delete("token");
        return "Account logged out";
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
            logService.Log(user.Username, "Registration- " + creationStatus, "Data Store", "Error");
            return "Database Error";
        }
        else if (creationStatus == "Email Failed To Send")
        {
            logService.Log(user.Username, "Registration- Email Failed To Send", "Business", "Error");
            return "Email Failed To Send";
        }
        else if (creationStatus != "Email Pending Confirmation")
        {
            logService.Log(user.Username, "Registration- " + creationStatus, "Business", "Information");
        }
        else
        {
            logService.RegistrationLog(user.Username, "Verfication Email Sent", "Business", "Information");
        }
        return creationStatus;
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
        if (verfied == "Account Verified")
        {
            logService.Log(Username, "Registration- Email Verified ", "Business", "Information");
            return verfied;
        }
        logService.RegistrationLog(Username, "Registration- Email Verified ", "Data Store", "Verified");
        return verfied;
    }


}




