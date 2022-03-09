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


using The6Bits.EmailService;
using The6Bits.BitOHealth.DAL.Implementations;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class RecordsService
    {
        private IRepositoryAuth<string> _MHD;
        private IDBErrors _DBErrors;
        private ISMTPEmailServiceShould _EmailService;

        public RecordsService(IRepositoryAuth<string> daotype, IDBErrors DbError, ISMTPEmailServiceShould EmailService)
        {
            _DBErrors= DbError;
            _MHD = daotype;
            _EmailService= EmailService;


        }

        public RecordsService(RecordsMsSqlDAO recordsMsSqlDao)
        {

        }


        // CHECK IF FILE SIZE IS OK
        // 0.5 MB MIN - 12 MB MAX   
        public string ValidateFileSizeRecords(string fileName, string username)
        {
            //string fileName;
            //string username;
            //int fileSize = fileName.VerifyFileSizeRecords(user);
            int fileSize = fileName.Length;
            if (fileSize < 500000)
        {
                return fileName + "File size too small. Try again";
            }
            if (fileSize > 112000000)
            {
                return fileName + "File size too large. Try again";
            }

        }

        // CHECK TO SEE IF FILE HAS CORRECT 
        public string VerifyFileTypeRecords(string fileName, string userName, string filePath)
        {
            string correctFile = Path.GetExtension(filePath);
            if(correctFile == ".pdf")
            {
                return "Upload Successful";

            }
            else if(correctFile == "jpg")
            {
                return "Upload Successful";
            }
            else
            {
                return "Invalid File Type";
            }
                
        }

        public string VerifyFileNameRecords(string fileName, string username, string filePath)
        {
            string correctFileName = Path.GetFileName(fileName.correctFileName);
            char [] invalidFilenameChars = Path.GetInvalidFileNameChars();
            char [] InvalidPathChars = Path.GetInvalidPathChars();
            
            
            if (File.Exists(correctFileName))
            {
                foreach (char someChar in correctFileName)
                {
                    if (Char.IsWhiteSpace(someChar))
                    {
                        return "Invalid file name.";
                    }
                    else if (someChar = Path.GetInvalidFileNameChars)
                    {
                        return "invalid file name";
                    }
                }
            }
            else
            {
                return "File Name does not exist";
            }

        }


        public string VerifySystemStorageRecords(string fileName, string username, string filePath)
        {

        }


    }

    
}
