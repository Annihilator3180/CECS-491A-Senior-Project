using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("News")]
public class HotTopicsController : ControllerBase
{
    private IAuthenticationService authenticationService1;
    private HotTopicsManager _HTM;
    private LogService logService;
    // private IAuthenticationService _auth;
    public HotTopicsController(IAuthenticationService authenticationService, ILogDal logDao, HotTopicsService hotTopic)
    {
        _HTM = new HotTopicsManager(hotTopic);
        logService = new LogService(logDao);

        // _auth = authenticationService;
    }

    [HttpGet("viewHT")]
    public async Task<string> viewNews()
    {
        var news = _HTM.ViewHT();
        //if (news.Equals("Database"))
        //{
        //    logService.Log(key, news, "View News Error ");
        //} 
        //else
        //{
        //    logService.Log(key, "News " + news);
        //}
        return await news;
    }

    public async Task<string> ViewHT()
    {
        bool isValid = authenticationService1.ValidateToken(Request.Headers["Authorization"]);
        string username = authenticationService1.getUsername(Request.Headers["Authorization"]);

        if (!isValid)
        {
            return "invalid token";
        }
        return await _HTM.ViewHT();
    }
}