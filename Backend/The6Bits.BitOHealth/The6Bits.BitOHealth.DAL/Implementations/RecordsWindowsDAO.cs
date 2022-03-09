using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using Dapper;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class RecordsWindowsDAO : IRecordsPC<string>
    {
        private string _connectString;

        public RecordsWindowsDao(string connectstring)
        {
            _connectString = connectstring;
        }

        public string VerifySystemStorageRecords(string fileName, string username, string filePath)
        {
        
        }
    }
}
