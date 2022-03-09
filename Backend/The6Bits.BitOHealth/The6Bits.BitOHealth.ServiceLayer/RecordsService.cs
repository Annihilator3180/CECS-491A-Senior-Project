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

        public RecordsService(AccountMsSqlDao accountMsSqlDao)
        {

        }


        // CHECK IF FILE SIZE IS OK
        // 0.5 MB MIN - 12 MB MAX   
        public string ValidateFileSizeRecords(User user)
        {
            string fileName;
            string username;
            int fileSize;

            if (fileSize < 0.5 MB)
        {
                return fileName + "File size too small. Try again";
            }
            if (fileSize > 12 MB){
                return fileName + "File size too large. Try again";
            }

        }

        // CHECK TO SEE IF FILE HAS CORRECT 
        public string VerifyFileTypeRecords(string fileName, string userName, string filePath)
        {
            return "null";
        }













    }

    
}
