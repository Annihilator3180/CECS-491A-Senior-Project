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
    public class RecordsWindowsDAO : IRecordsPC
    {
        private string _connectString;

        public RecordsWindowsDAO()
        {
        }
        public RecordsWindowsDAO(string connectstring)
        {
            _connectString = connectstring;
        }

        // Windows DAO access for VerifySystemsStorageRecords
        public string VerifySystemStorageRecords(string fileName, string username, string filePath)
        {
            try
            {
                string query = "select MHRecords ";
                return null;
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }
        // Create Records for Win DAO
        public string CreateRecords(string recordName, string username)
        {
            try
            {
                string query = "Insert into MHRecords (Username, Records) " +
                    "values (@Username, @Records)";
                return null;
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }
        // FOR WIN DAO
        public string UploadRecordsWinDao(string fileName, string username, string filePath)
        {
            try
            {
                string query = "";
                return null;
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }
    }
}
