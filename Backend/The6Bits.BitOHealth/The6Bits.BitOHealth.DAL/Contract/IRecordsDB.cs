using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public class IRecordsDB<T>
    {
        //public string VerifyFileNameRecords(string fileName, string username, string filePath);

        public string VerifySystemStorageRecords(string fileName, string username, string filePath);

        public string CreateRecords(string recordName, string username, string filePath);


    }
}
