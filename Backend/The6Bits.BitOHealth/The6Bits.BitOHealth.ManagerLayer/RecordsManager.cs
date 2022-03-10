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
        private ISMTPEmailServiceShould _EmailService;
        private IConfiguration _config;

    }

    public RecordsManager(IRepositoryUM<string> authdao, IAuthenticationService authenticationService, IDBErrors dbError, ISMTPEmailServiceShould email, IConfiguration config)
    {
        _iDBErrors = dbError;
        _EmailService = email;
        _authentication = authenticationService;
        _MHS = new RecordsService(authdao, dbError, email);
        _config=config;
    }

    // CHECK IF FILE SIZE IS OK
    // 0.5 MB MIN - 12 MB MAX   
    public string ValidateFileSizeRecords(string fileName, string username)
    {
        string fileName;
        string username;
        int fileSize;

        if(fileSize < 0.5 MB)
        {
            return fileName + "File size too small. Try again";
        }
        if(fileSize > 12 MB){
            return fileName + "File size too large. Try again";
        }
        
    }

    public string VerifyFileTypeRecords(string fileName,string userName,string filePath)
    {
        return "null";
    }
}
