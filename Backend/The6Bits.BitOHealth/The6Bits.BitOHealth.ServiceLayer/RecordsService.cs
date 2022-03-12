using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;
using System.Data.SqlClient;
using Dapper;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.EmailService;
using The6Bits.BitOHealth.DAL.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class RecordsService
    {
        private IRecordsDB _MHD;
        private IDBErrors _DBErrors;
        private ISMTPEmailService _EmailService;
        private IConfiguration _config;

        public RecordsService(IRecordsDB daotype, IDBErrors DbError, ISMTPEmailService EmailService, IConfiguration config)
        {
            _DBErrors= DbError;
            _MHD = daotype;
            _EmailService= EmailService;
            _config = config;
        }

        public RecordsService(RecordsMsSqlDAO recordsMsSqlDao)
        {
        }

        // CHECK IF FILE SIZE IS OK
        // 0.5 MB MIN - 12 MB MAX   
        // 500000 Bytes - 112000000 Bytes
        // Todo: DONE
        public string ValidateFileSizeRecords(string fileName, string username, int fileSize)
        {
            //string fileName;
            //string username;
            //int fileSize = fileName.VerifyFileSizeRecords(user);
            //int fileSize2 = fileName.Length;
            //int fileSize2 = _MHD.ValidateFileSizeRecords(fileName, username, fileSize);
            int fileSize2 = Int32.Parse(ValidateFileSizeRecords(fileName, username, fileSize));

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

        // CHECK TO SEE IF FILE HAS CORRECT
        // Todo: Done
        public string VerifyFileTypeRecords(string fileName, string userName, string filePath)
        {
            string correctFile = Path.GetExtension(filePath);
            //string correctFile2 = _MHD.VerifyFileTypeRecords(correctFile, userName, filePath);
            if (correctFile == ".pdf")
            {
                return "Upload Successful";

            }
            else if (correctFile == ".jpg")
            {
                return "Upload Successful";
            }
            else
            {
                return "Invalid File Type";
            }

        }

        // CHECKS TO SEE IF FILE NAME IS VALID
        // Todo: Done
        public string VerifyFileNameRecords(string fileName, string username, string filePath)
        {
            string correctFileName = Path.GetFileName(fileName);
            //string correctFileName2 = _MHD.VerifyFileNameRecords(correctFileName, username, filePath); //use _MHD when accessing dao
            char[] invalidFilenameChars = Path.GetInvalidFileNameChars();

            foreach (char someChar in Path.GetInvalidFileNameChars())
            {
                if (Char.IsWhiteSpace(someChar))
                {
                    return "Invalid file name.";
                }
                else if (correctFileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                {
                    return "Invalid file name, contains special characters";
                }
                else
                {
                    return "valid filename: " + fileName;
                }
            }
            return null;

        }

        // CHECKS TO WINDAO IF THERE IS ENOUGH STORAGE
        // Todo : Fix
        public string VerifySystemStorageRecords(string fileName, string username, string filePath)
        {
            string ver = _MHD.VerifySystemStorageRecords(fileName, username, filePath);

            DriveInfo drive = new DriveInfo("C");

            if (drive.IsReady)
            {
                double freeSpace = ((drive.AvailableFreeSpace / (double)drive.TotalSize) * 100);
                return freeSpace.ToString();
            }
            else
            {
                return "Upload failed";
            }


            return null;
        }

        // CHECKS TO SEE IF RECORD NAME MEETS LENGTH REQUIREMENT
        // Todo : Done
        public string CreateRecords(string recordName, string username)
        {
            string nam = _MHD.CreateRecords(recordName, username);
            if (nam.Length < 0 || nam.Length > 100)
            {
                return recordName + " invalid. Record name needs to be 1-100 characters long.";
            }
            else if (nam.Length < 0 || nam.Length > 100)
            {
                return _DBErrors.DBErrorCheck(int.Parse(nam));
            }
            else
            {
                return recordName + " valid.";
            }

        }
        // CHECKS TO SEE IF RECORDS UPLOADED TO WINDOWS SERVER/MACHINE  
        //  Todo : Done
        public string UploadRecordsWinDao(string fileName, string username, string filePath)
        {
            string fileToWindows = _MHD.UploadRecordsWinDao(fileName, username, filePath);
            string filePath2 = "C:\\Users\\Owner\\Documents\\";

            if (File.Exists(fileToWindows) == true && File.Exists(filePath2) == true)
            {
                return fileToWindows + " uploaded successfully to Windows DAO";

            }
            else
            {
                return fileToWindows + " not uploaded successfully to Windows DAO";
            }
            return null;
        }

    }

    
}
