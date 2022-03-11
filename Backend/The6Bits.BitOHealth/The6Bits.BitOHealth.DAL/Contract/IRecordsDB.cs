using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IRecordsDB
    {
        //public string ValidateFileSizeRecords(string fileName, string username, int fileSize);
        //public string VerifyFileTypeRecords(string fileName, string userName, string filePath);
        //public string VerifyFileNameRecords(string fileName, string username, string filePath);
        public string VerifySystemStorageRecords(string fileName, string username, string filePath);
        public string CreateRecords(string recordName, string username);
        public string UploadRecordsWinDao(string fileName, string username, string filePath);

    }
}
