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
}