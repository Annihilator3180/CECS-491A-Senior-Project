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
    public class RecordsMsSqlDAO : IRecordsDB
    {
        private string _connectString;

        public RecordsMsSqlDAO()
        {
        }
        public RecordsMsSqlDAO(string connectstring)
        {
            _connectString = connectstring;
        }

        // DAO access for VerifySystemsStorageRecords
        public string VerifySystemStorageRecords(string fileName, string username, string filePath)
        {
            try
            {
                string query = "Insert into MHRecords (Username, Records) " +
                    "values (@Username, @Records)";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query,
                        new
                        {
                            Username = username,
                            filename = fileName,

                        }
                        );
                    return "0";

                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();

            }

        }

        // Dao access for CreateRecords
        public string CreateRecords(string recordName, string username)
        {
            try
            {
                string query = "Insert into MHRecords (Username, Records) " +
                    "values (@Username, @Records)";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query,
                        new
                        {
                            Username = username,
                            recordname = recordName,
                            
                        }
                        );
                    return "0";

                }
            }
            catch(SqlException ex)
            {
                return ex.Number.ToString();

            }
            
        }

        public string UploadRecordsWinDao(string fileName, string username, string filePath)
        {
            try
            {
                string query = "";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query,
                        new
                        {
                            Username = username,
                            filename = fileName,

                        }
                        );
                    return "0";

                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();

            }

        }
    }
}
