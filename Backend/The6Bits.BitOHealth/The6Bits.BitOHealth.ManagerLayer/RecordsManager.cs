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

    public RecordsManager(IRecordsDB daotype, IAuthenticationService authenticationService, IDBErrors dbError, ISMTPEmailServiceShould email, IConfiguration config)
    {
        _iDBErrors = dbError;
        _EmailService = email;
        _authentication = authenticationService;
        _MHS = new RecordsService(daotype, dbError, email);
        _config=config;
    }

    // CHECK IF FILE SIZE IS OK
    // 0.5 MB MIN - 12 MB MAX   
    public string ValidateFileSizeRecords(string fileName, string username, int fileSize)
    {
        //string fileName;
        //string username;
        //int fileSize = fileName.VerifyFileSizeRecords(user);
        //int fileSize2 = fileName.Length;
        //int fileSize2 = _MHD.ValidateFileSizeRecords(fileName, username, fileSize);
        int fileSize2 = _MHS.Int32.Parse(ValidateFileSizeRecords(fileName, username, fileSize));

        if (fileSize2 < 500000)
        {
            return fileName + "File size too small. Try again";
        }
        else if (fileSize2 > 112000000)
        {
            return fileName + "File size too large. Try again";
        }
        else
        {
            return fileName + " is " + fileSize;
        }

    }

    public string VerifyFileTypeRecords(string fileName,string userName,string filePath)
    {
        return "null";
    }
}
