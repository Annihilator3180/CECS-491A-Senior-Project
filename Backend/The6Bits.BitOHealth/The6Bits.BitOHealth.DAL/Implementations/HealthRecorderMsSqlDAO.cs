using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;

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
                    else if (dailyRecord > 15)
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
        public string SaveRecord(string record, DateTime timeSaved, string username, string categoryName, string recordName, string secondRecord)
        {
            //consider unique record names
            try
            {
                string query = "INSERT INTO HealthRecorder(record, secondRecord, timeSaved, username, categoryName, recordName) values(@record, @secondRecord," +
                    "@timeSaved, @username, @categoryName, @recordName)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    int result = conn.Execute(query, new {record = record, secondRecord = secondRecord, timeSaved = timeSaved, 
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
        public List<HealthRecorderRecordModel> GetRecords(string username, int lastRecordIndex)
        {
            /*
            maybe return list of record objects
            select each column of data and then create new object, insert into list
            https://www.learndapper.com/selecting-multiple-rows
            select* from HealthRecorder
            ORDER BY timeSaved ASC 
            OFFSET 1 ROWS
            FETCH NEXT 2 ROWS ONLY
            */
            List<HealthRecorderRecordModel> records = new List<HealthRecorderRecordModel>();
            try
            {
                string query = "Select* from HealthRecorder where username = @username ORDER BY timeSaved DESC OFFSET @lastRecordIndex ROWS FETCH NEXT 10 ROWS ONLY";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    var getRecords = conn.Query<HealthRecorderRecordModel>(query, new {username = username, lastRecordIndex = lastRecordIndex});
                    foreach(var r in getRecords)
                    {
                        records.Add(r);
                    }
                    return records;
                }
            }
            catch(SqlException ex)
            {
                string errorCode = ex.Number.ToString();
                HealthRecorderRecordModel error = new HealthRecorderRecordModel();
                error.ErrorCode = errorCode;
                records.Add(error);
                return records;
            }
        }

    }
}
