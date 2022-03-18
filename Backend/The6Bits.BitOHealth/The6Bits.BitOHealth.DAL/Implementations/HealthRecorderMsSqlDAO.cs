using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class HealthRecorderMsSqlDAO : IRepositoryHealthRecorderDAO
    {
        private string _connectionString;

        public HealthRecorderMsSqlDAO(string conn)
        {
            _connectionString = conn;
        }
        public string ValidateUserRecordLimit(string username)
        {
            try
            {
                string query = "select count(record) from HealthRecorder where username = @username";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    int recordCount = conn.ExecuteScalar<int>(query, new {username = username}) ;
                    conn.Close();
                    
                    if (recordCount < 1000)
                    {
                        return "under";
                    }
                    return "User over total record limit";

                }

            }
            catch(SqlException ex)
            {
                return ex.Number.ToString();
            }
        }
        public string ValidateUserDailyRecordLimit(string username, DateTime now)
        {
            try
            {
                string query = "select count(record) from HealthRecorder where username = @username AND @now >= DATEADD (day, -1, GETDATE())";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    int dailyRecordCount = conn.ExecuteScalar<int>(query, new { username = username, now = now });
                    if (dailyRecordCount < 2)
                    {
                        return "under";
                    }
                    return "User over daily record limit";
                }
            }
            catch(SqlException ex)
            {
                return ex.Number.ToString();
            }
        }
        public string SaveRecord(string record, DateTime timeSaved, string username, string categoryName, string recordName)
        {
            try
            {
                string query = "INSERT INTO HealthRecorder(record, timeSaved, username, categoryName, recordName) values(@record," +
                    "@timeSaved, @username, @categoryName, @recordName)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    int result = conn.Execute(query, new {record = record, timeSaved = timeSaved, 
                        username = username, categoryName = categoryName, recordName = recordName});
                    conn.Close();

                }
                return "saved";
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

    }
}
