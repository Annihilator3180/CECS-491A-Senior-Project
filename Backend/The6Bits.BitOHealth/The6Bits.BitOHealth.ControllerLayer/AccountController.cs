using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Account")]
public class AccountController : ControllerBase
{
    private AccountManager _AM;
    private LogService logService;
    public AccountController(IRepositoryAuth<string> authdao ,ILogDal logDao, IAuthenticationService authenticationService)
    {
        _AM = new AccountManager(authdao,authenticationService);
        logService = new LogService(logDao);
    }

    [HttpPost("Login")]
    public string Login(LoginModel acc)
    {
        

        var jwt =  _AM.Login(acc);
        var parts = jwt.Split('.');
        
        //TODO:FIX IF STATMENT TO SOMETHING BETTER
        //TODO:ADD LOGS
        if (parts.Length==3)
        {
            Response.Cookies.Append(
                "token",
                jwt);
            logService.Log(acc.Username,"Logged In", "Info","Business" );
        }
        else
        {
            string loginfail = "Log In Fail";
            logService.Log(acc.Username,loginfail+" "+jwt, "Info","Business" );

        }

        return jwt;

    }
    
    [HttpPost("OTP")]
    public string SendOTP(string username)
    {
        
        var otp =  _AM.SendOTP(username);
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
    
}