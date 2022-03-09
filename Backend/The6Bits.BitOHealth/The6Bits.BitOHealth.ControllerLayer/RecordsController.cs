using System;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Authorization.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Http;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL.Contract;
namespace The6Bits.BitOHealth.ControllerLayer
{
    public class RecordsController : ControllerBase
    {
        private IAuthenticationService authenticationService1;
        private RecordsManager _MHM;
        private LogService logService;
        private string Username;
        private bool isValid;
        private IAuthenticationService _authentication;
    }

    public RecordsController(IRepositoryAuth<string> authdao, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors, ISMTPEmailServiceShould EmailService, IConfiguration config)
    {
        _MHM = new RecordsManager(authdao, authenticationService,dbErrors,EmailService,config);
        
        logService = new LogService(logDao);
        _dbErrors = dbErrors;
        _EmailService = EmailService;
        authenticationService1 = authenticationService;
        _config = config;
        
    }

    [HttpPost("CreateRecords")]
    
    public string CreateRecords(User u)
    {
        isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);
        string userName = _authenication.getUsername(Request.Headers["Authorizatoin"]);

        if (isValid == null) 
        { 
            


            
        }
    }


}
