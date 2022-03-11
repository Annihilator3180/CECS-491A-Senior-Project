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


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class RecordsManager
    {
        private IAuthenticationService _authentication;
        private RecordsService _MHS;
        private IDBErrors _iDBErrors;
        private ISMTPEmailService _EmailService;
        private IConfiguration _config;



        public RecordsManager(IRecordsDB daotype, IAuthenticationService authenticationService, IDBErrors dbError, ISMTPEmailService email, IConfiguration config)
        {
            _iDBErrors = dbError;
            _EmailService = email;
            _authentication = authenticationService;
            _config = config;
            _MHS = new RecordsService(daotype, dbError, email, config);

        }


        public string CreateRecords(string recordName, string username)
        {
            // CHECKS IF FILE SIZE IS OK
            // Todo : Fix
            var response = _MHS.ValidateFileSizeRecords(fileName, username, fileSize);
            if (response.Contains("Try again"))
            {
                return response;
            }

            // CHECKS TO SEE IF FILE HAS CORRECT
            // Todo : Fix
            var responseFileType = _MHS.VerifyFileTypeRecords(fileName, username, filePath);
            if (responseFileType.Contains("Upload Successful"))
            {
                return responseFileType;
            }
            else
            {
                return responseFileType;
            }

            // CHECKS TO SEE IF FILE NAME IS VALID
            // Todo : Fix
            var responseFileName = _MHS.VerifyFileNameRecords(fileName, username, filePath);
            if (responseFileName.Contains("Invalid file name."))
            {
                return responseFileName;
            }
            else if (responseFileName.Contains("Invalid file name, contains special characters"))
            {
                return responseFileName;
            }
            else
            {
                return responseFileName;
            }

            // CHECKS TO WINDAO IF THERE IS ENOUGH STORAGE
            // Todo : Fix
            var responseSystemStorage = _MHS.VerifySystemStorageRecords(fileName, username, filePath);


            // CHECKS TO SEE IF RECORD NAME MEETS LENGTH REQUIREMENT
            // Todo : Done
            var responseCreateRecords = _MHS.CreateRecords(recordName, username);
            if (responseCreateRecords.Contains("invalid"))
            {
                return responseCreateRecords;
            }
            else if (responseCreateRecords.Contains("valid"))
            {
                return responseCreateRecords;
            }
            else
            {
                return responseCreateRecords;
            }

            // CHECKS TO SEE IF RECORDS UPLOADED TO WINDOWS SERVER/MACHINE 
            var responseUploadRecords = _MHS.UploadRecordsWinDao(fileName, username, filePath);

        }
    }


    

}
