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

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    private AccountManager _AM;
    private LogService logService;
    private IDBErrors _dbErrors;
    public AccountController(IRepositoryAuth<string> authdao ,ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors)
    {
        _AM = new AccountManager(authdao,authenticationService);
        logService = new LogService(logDao);
        dbErrors = _dbErrors;
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



    [HttpGet("Register")]
    public string CreateAccount(User user)
    {

        String emailStatus = _AM.CreateAccount(user);
        return "0";
    }
    [HttpGet("VerifyAccount")]
    public string VerifyAccount(String Code, String Username)
    {
        if (Code == "0")
        {
            return "Input";
        }
        return "no";
    }


}



