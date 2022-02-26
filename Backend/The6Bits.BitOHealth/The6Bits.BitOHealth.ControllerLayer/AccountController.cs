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

    //TODO: Finish up
    [HttpPost("Logout")]
    public string Logout()
    {
        Response.Cookies.Delete("token");
        return null;
    }


 //   public void DeleteCookie(object sender, EventArgs e)
 //   {
 //       //HttpCookie httpCookie = new HttpCookie();
  //      HttpCookie httpCookie = Request.Cookies.Get("Cookie");
  //      httpCookie.Expires = DateTime.Now.AddDays(-1d);
  //      Response.Cookies.Append("cookie",httpCookie);
 //   }
    
    //Sets the JWT string to ""
    public string DeleteToken(LoginModel acc)
    {
        var jwt = _AM.Login(acc);
        return jwt = "";

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