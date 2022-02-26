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

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
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
    }

    [HttpPost("Login")]
    public string Login(LoginModel acc)
    {
        

        var jwt =  _AM.Login(acc);
        var parts = jwt.Split('.');
        
        //TODO:FIX IF STATMENT TO SOMETHING BETTER
        //TODO:ADD LOGS
        //TODO:2FA 
        if (parts.Length==3)
        {
            
            Response.Cookies.Append(
                "token",
                jwt);
        }

        return jwt;

    }
    
    [HttpPost("OTP")]
    public string SendOTP(string username)
    {
        
        var otp =  _AM.SendOTP(username);
        return otp;
    }



    [HttpPost("Register")]
    public string CreateAccount(User user)
    {

        String CreationStatus = _AM.CreateAccount(user);
        if (CreationStatus.Contains("Database"))
        {
            logService.Log(user.Username, "Registration- " + CreationStatus, "Data Store", "Error");
            return "Database Error";
        }
        else if (CreationStatus == "Email Failed To Send")
        {
            logService.Log(user.Username, "Registration- Email Failed To Send", "Business", "Error");
            return "Email Failed To Send";
        }
        else if (CreationStatus != "Email Pending Confirmation") {
            logService.Log(user.Username, "Registration- "+CreationStatus, "Business", "Information");
                }
        else
        {
            logService.Log(user.Username, "Verfication Email Sent", "Business", "Information");
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


}



