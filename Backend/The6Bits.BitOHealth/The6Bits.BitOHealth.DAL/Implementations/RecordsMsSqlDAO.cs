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
    public class RecordsMsSqlDAO : IRecordsDB<string>
    {
        private string _connectString;



        public RecordsMsSqlDao(string connectstring)
        {
            _connectString = connectstring;
        }
        // async/await
        public string UsernameExists(string username)
        {
            try
            {
                string query = $"select count(@Username) from Accounts where Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query, new { Username = username });
                    if (count != 0)
                    {
                        return "username exists";
                    }
                    return "username not found";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }


        }

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
    }
}
