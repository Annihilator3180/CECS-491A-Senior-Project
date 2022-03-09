using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public class IRecordsPC<T>
    {
        public string VerifySystemStorageRecords(string fileName, string username);

        public string CreateRecords(string recordName, string username);

        public string UploadRecordsWinDao(string fileName, string username, string filePath);

    }
}
