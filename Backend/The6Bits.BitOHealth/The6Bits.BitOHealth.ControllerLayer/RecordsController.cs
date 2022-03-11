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
using The6Bits.DBErrors;
using The6Bits.EmailService;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace The6Bits.BitOHealth.ControllerLayer
{
    public class RecordsController : ControllerBase
    {
        private IAuthenticationService _authenticationService;
        private IConfiguration _config;
        private RecordsManager _MHM;
        
        private ISMTPEmailService _EmailService;
        private IDBErrors _dbErrors;
        private string Username;
        private bool isValid;
        private IAuthenticationService _authentication;


        public RecordsController(IRecordsDB daoType, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors,
            ISMTPEmailService EmailService, IConfiguration config)
        {
            _MHM = new RecordsManager(daoType, authenticationService, dbErrors, EmailService, config, logDao);
            
            _dbErrors = dbErrors;
            _EmailService = EmailService;
            _authenticationService = authenticationService;
            _config = config;

        }

        [HttpPost("CreateRecords")]
        public string CreateRecords(String recordName, String username)
        {
            //isValid = _authentication.ValidateToken(Request.Headers["Authorization"]);
            //string userName = _authentication.getUsername(Request.Headers["Authorizatoin"]);
            string namingStatus = _MHM.CreateRecords(recordName, username);

            if (namingStatus.Contains("invalid"))
            {
                //logService.Log(username, "Record name created failed", "Information","Business");
                return "Database Error";
            }
            else if (namingStatus.Contains("valid"))
            {
                //logService.Log(username, "Record created/saved successfully", "Information", "Business");
                return namingStatus;
            }
            else
            {
                //logService.Log(username, "Record name created failed - length requirements", "Information", "Business");
            }
            return namingStatus;
        }


        // ADD Authenticate.IsValidToken(token) : Bool

        // ADD Authenticate.getUserName(token : string) : string




    //    [HttpGet("ValidateFileSizeRecords")]
    //    public string ValidateFileSizeRecords(String fileName, String username, String filePath)
    //    {
    //        string sizeStatus = _MHM.ValidateFileSizeRecords(fileName, username, filePath);
    //        if (sizeStatus.Contains("Try again"))
    //        {
    //            //logService.Log(username, "Invalid Size: " + fileName, "Information", "Business");
    //            return "Cannot upload file because it did not meet requirements";
    //        }
           
    //        logService.Log(Username, "Record crated/saved successfully", "Information", "Business");
    //        return sizeStatus;
    //    }

    //    [HttpGet("VerifyFileTypeRecords")]
    //    public string VerifyFileTypeRecords(String fileName, String username, String filePath)
    //    {
    //        string fileTypeStatus = _MHM.VerifyFileTypeRecords(fileName, username, filePath);
    //        if (fileTypeStatus.Contains("Invalid"))
    //        {
    //            logService.Log(username, "Invalid Type - File Type", "Information", "Business");
    //            return "invalid file type";
    //        }
    //        else
    //        {
    //            logService.Log(username, "Upload Successful - File Type", "Information", "Business");
    //        }
    //        logService.Log(username, "Upload Successful - File Type", "Information", "Business");
    //        return fileTypeStatus;
    //    }

    //    [HttpGet("VerifyFileNameRecords")]
    //    public string VerifyFileNameRecords(String fileName, String username, String filePath)
    //    {
    //        stromg fileNameStatus
    //    }
    //}

}
