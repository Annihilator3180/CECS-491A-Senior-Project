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

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Health Locator")]
public class HealthLocatorController : ControllerBase
{
    // Todo: Fix code
    private IAuthenticationService _authenticationService;
    private HealthLocatorManager _HLM;
    private LogService _logService;

    // Todo: Fix HLController
    public HealthLocatorController(IAuthenticationService _authenticationService, ILogDal logDao, HealthLocatorService healthLocator)
    {
        _HLM = new HealthLocatorManager(healthLocator);
        _logService = new LogService(logDao);
    }

    // Todo: Fix
    [HttpGet("searchHL")]
    public async Task<string> viewLocation()
    {
        var location = _HLM.ViewHL();
        return await location;
    }

    // Todo: Fix
    public async Task<string> ViewHL()
    {
        bool isValid = _authenticationService.ValidateToken(Request.Headers["Authorization"]);
        string username = _authenticationService.getUsername(Request.Headers["Authorization"]);

        if (!isValid)
        {
            return "invalid token";
        }
        return await _HLM.ViewHL();
    }
}
