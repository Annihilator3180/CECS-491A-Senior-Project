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
    [Consumes("application/json")]
    //Token
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
        }
        return jwt;
        
    }

    //TODO: Finish up
    [HttpPost("Logout")]
    public string Logout()
    {
        return null;
    }

    //FIX
    public string Logout(string token)
    {
        return null;
    }

    //Checks to see if the account has a Token or not
    public bool HasToken(string token) //FIX
    {
        if(token != "" || token != " ") //If there is a value in jwtToken return true
        {
            return true;
        }
        if(token == "" || token == " ") //If token is blank
        {
            return false;
        }
        return true;
    }

    
    public void DeleteCookie(object sender, EventArgs e)
    {
        //HttpCookie httpCookie = new HttpCookie();
        HttpCookie httpCookie = Request.Cookies.Get("Cookie");
        httpCookie.Expires = DateTime.Now.AddDays(-1d);
        Response.Cookies.Append("cookie",httpCookie);
    }
    
    //Sets the JWT string to ""
    public string DeleteToken(LoginModel acc)
    {
        var jwt = _AM.Login(acc);
        return jwt = "";

    }


}