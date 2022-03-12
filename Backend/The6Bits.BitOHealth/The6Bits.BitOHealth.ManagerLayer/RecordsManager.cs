using System;
using System.ComponentModel.Design;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging;
using The6Bits.Logging.Implementations;
using The6Bits.Authorization.Contract;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.Authentication.Contract;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using The6Bits.Logging.DAL.Contracts;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class RecordsManager
    {
        private IAuthenticationService _authentication;
        private RecordsService _MHS;
        private IDBErrors _iDBErrors;
        private ISMTPEmailService _EmailService;
        private IConfiguration _config;
        private LogService logService;


        public RecordsManager(IRecordsDB daotype, IAuthenticationService authenticationService, IDBErrors dbError,
            ISMTPEmailService email, IConfiguration config, ILogDal logDao)
        {
            _iDBErrors = dbError;
            _EmailService = email;
            _authentication = authenticationService;
            _config = config;
            _MHS = new RecordsService(daotype, dbError, email, config);
            logService = new LogService(logDao);

        }


        public string CreateRecords(string recordName, string username, string fileName, int fileSize, string filePath)
        {
            // CHECKS IF FILE SIZE IS OK
            // Todo : Done
            var response = _MHS.ValidateFileSizeRecords(fileName, username, fileSize);
            if (response.Contains("Try again"))
            {
                logService.Log(username, "Invalid Size: " + fileName, "Information", "Business");
                return response;
                
            }
            logService.Log(username, "Record crated/saved successfully", "Information", "Business");

            // CHECKS TO SEE IF FILE HAS CORRECT
            // Todo : Done
            var responseFileType = _MHS.VerifyFileTypeRecords(fileName, username, filePath);
            if (responseFileType.Contains("Upload Successful"))
            {
                logService.Log(username, "Record created/saved successfully", "Information", "Business");
                return responseFileType;
            }
            else
            {
                logService.Log(username, "Invalid File Type", "Information", "Business");
                return responseFileType;
            }

            // CHECKS TO SEE IF FILE NAME IS VALID
            // Todo : Done
            var responseFileName = _MHS.VerifyFileNameRecords(recordName, username, filePath);
            if (responseFileName.Contains("Invalid file name."))
            {
                logService.Log(username, "Invalid filename - whitespace", "Information", "Business");
                return responseFileName;
            }
            else if (responseFileName.Contains("Invalid file name, contains special characters"))
            {
                logService.Log(username, "Invalid filename - Special Characters", "Information", "Business");
                return responseFileName;
            }
            else
            {
                logService.Log(username, "Valid Filename", "Information", "Business");
                return responseFileName;
            }

            // CHECKS TO WINDAO IF THERE IS ENOUGH STORAGE
            // Todo : Done
            var responseSystemStorage = _MHS.VerifySystemStorageRecords(fileName, username, filePath);

            if(responseSystemStorage.Contains("Upload"))
            {
                logService.Log(username, "Upload failed - Win DAO", "Information", "Business");
                return responseSystemStorage;
            }
            else
            {
                return responseSystemStorage;
            }

            // CHECKS TO SEE IF RECORD NAME MEETS LENGTH REQUIREMENT
            // Todo : Done
            var responseCreateRecords = _MHS.CreateRecords(recordName, username);
            if (responseCreateRecords.Contains("invalid"))
            {
                logService.Log(username, "Record name needs to be 1-100 characters long", "Information", "Business");
                return responseCreateRecords;
            }
            else if (responseCreateRecords.Contains("valid"))
            {
                logService.Log(username, "Record name valid", "Information", "Business");
                return responseCreateRecords;
            }
            else
            {
                logService.Log(username, "DB Error - Internal Error", "Information", "Business");
                return responseCreateRecords;
            }

            // CHECKS TO SEE IF RECORDS UPLOADED TO WINDOWS SERVER/MACHINE 
            // Todo : Done
            var responseUploadRecords = _MHS.UploadRecordsWinDao(fileName, username, filePath);
            if (responseUploadRecords.Contains("uploaded successfully to Windows DAO"))
            {
                logService.Log(username, "Upload Successful to Win DAO", "Information", "Business");
                return responseUploadRecords;
            }
            else
            {
                logService.Log(username, "Upload not successful to Win DAO", "Information", "Business");
                return responseUploadRecords;
            }
        }
    }
}
