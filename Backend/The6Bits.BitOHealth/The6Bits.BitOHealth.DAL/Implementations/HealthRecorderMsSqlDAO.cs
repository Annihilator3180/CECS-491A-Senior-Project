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

        public string ValidateUserRecordLimits(string username)
        {
            try
            {
                string query = "select count(record) as totalRecord, sum (case when timeSaved >= DATEADD(day, -1, GETDATE()) then 1 else 0 end) as dailyRecord" +
                    " from HealthRecorder where username = @username";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    //query return an enumerable , we only want one row so we put .single at the end,
                    //return type is dynamic so we can refer to column name defined in select statement
                    var limits = conn.Query(query, new { username = username}).Single();
                    var totalRecord = limits.totalRecord;
                    var dailyRecord  = limits.dailyRecord;

                    if (totalRecord > 1000)
                    {
                        return "over record limit";
                    }
                    else if (dailyRecord > 2)
                    {
                        return "over daily limit";
                    }
                    return "under limit";
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
