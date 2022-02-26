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
    private JWTAuthenticationService _JWTAuthenticationService;
    public AccountController(IRepositoryAuth<string> authdao ,ILogDal logDao, IAuthenticationService authenticationService)
    {
        _AM = new AccountManager(authdao,authenticationService);
        logService = new LogService(logDao);
        _JWTAuthenticationService = new JWTAuthenticationService();
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

    [HttpPost("Logout")]
    public string Logout()
    {
        Response.Cookies.Delete("token");
        return "Account logged out";
    }
    // JSON 
    [HttpPost("Delete")]
    public string DeleteAccount(string token)
    {

        string del =  _AM.DeleteAccount(token);
        Response.Cookies.Delete(token);
        return del;
    }


 //   public void deletecookie(object sender, eventargs e)
 //   {
 //       //httpcookie httpcookie = new httpcookie();
  //      httpcookie httpcookie = request.cookies.get("cookie");
  //      httpcookie.expires = datetime.now.adddays(-1d);
  //      response.cookies.append("cookie",httpcookie);
 //   }
    
}