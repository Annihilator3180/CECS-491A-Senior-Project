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
        public string SaveRecord(string record, string username, string categoryName, string recordName, string secondRecord)
        {
            //consider unique record names
            try
            {
                string query = "INSERT INTO HealthRecorder(record1, record2, timeSaved, username, categoryName, recordName) values(@record, @secondRecord," +
                    "CURRENT_TIMESTAMP, @username, @categoryName, @recordName)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    int result = conn.Execute(query, new
                    {
                        record = record,
                        secondRecord = secondRecord,
                        username = username,
                        categoryName = categoryName,
                        recordName = recordName
                    });
                    conn.Close();

                }
                return "saved";
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

        public string ValidateUserRecordRequest(string username, string recordName)
        {
            try
            {
                string query = "select count(*) as totalRecord, coalesce(sum(case when timeSaved >= DATEADD(day, -1, GETDATE()) then 1 else 0 end),0) as dailyRecord, coalesce(sum(case when recordName = @recordName then 1 else 0 end),0) as duplicateRecord " +
                     "from HealthRecorder where username = @username";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    //query return an enumerable , we only want one row so we put .single at the end,
                    //return type is dynamic so we can refer to column name defined in select statement
                    var limits = conn.Query(query, new { username = username, recordName = recordName}).Single();
                    var totalRecord = limits.totalRecord;
                    var dailyRecord  = limits.dailyRecord;
                    var duplicateRecord = limits.duplicateRecord;

                    if (totalRecord > 1000)
                    {
                        return "over record limit";
                    }
                    else if (dailyRecord > 15 )
                    {
                        return "over daily limit";
                    }
                    else if (duplicateRecord != 0)
                    {
                        return "duplicate record name";
                    }
                    return "valid request";
                }
            }
            catch(SqlException ex)
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
                string query = "Select* from HealthRecorder where username = @username ORDER BY timeSaved DESC OFFSET @lastRecordIndex ROWS FETCH NEXT 100 ROWS ONLY";
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
        public HealthRecorderResponseModel ValidateRecordExists(HealthRecorderRequestModel request, HealthRecorderResponseModel response, string username)
        {
            string recordName = request.RecordName;
            string categoryName = request.CategoryName;
            try
            {
                string query = "Select count(*) from HealthRecorder where username = @username AND recordName = @recordName AND categoryName = @categoryName";
                using (SqlConnection conn = new SqlConnection(_connectionString)) { 
                    int result = conn.ExecuteScalar<int>(query, new {username = username, recordName = recordName, categoryName = categoryName});
                    conn.Close();
                    response.Data = result.ToString();
                    return response;
                }
            }
            catch (SqlException ex)
            {
                response.ErrorMessage = ex.Number.ToString();
                return response;
            }
        }
        public HealthRecorderResponseModel DeleteRecord(HealthRecorderRequestModel request, HealthRecorderResponseModel response, string username)
        {
            string recordName = request.RecordName;
            string categoryName = request.CategoryName;
            try
            {
                string query = "delete from HealthRecorder where username = @username AND recordName = @recordName AND categoryName = @categoryName";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    int result = con.Execute(query, new {username = username, recordName = recordName,categoryName = categoryName});
                    con.Close();
                    response.Data = result.ToString();
                    return response;
                }
            }
            catch(SqlException ex)
            {
                response.ErrorMessage = ex.Number.ToString();
                return response;
            }
        }
        public HealthRecorderViewRecordModel SearchRecord(HealthRecorderRequestModel request, HealthRecorderViewRecordModel response, string username)
        {
            string recordName = request.RecordName;
            string categoryName = request.CategoryName;
            List<HealthRecorderRecordModel> records = new List<HealthRecorderRecordModel>();

            try
            {
                string query = "select* from HealthRecorder where username = @username AND (recordName = @recordName OR categoryName = @categoryName)";
                using(SqlConnection conn = new SqlConnection(_connectionString))
                {
                    var getRecords =conn.Query<HealthRecorderRecordModel>(query, new {username = username,recordName = recordName, categoryName = categoryName});
                    foreach (var r in getRecords)
                    {
                        records.Add(r);
                    }
                    response.Records = records;
                    return response;
                }
            }
            catch (SqlException ex)
            {
                response.ErrorMessage = ex.Number.ToString();
                return response;
            }


        }

        public HealthRecorderExportModel ExportRecord(HealthRecorderRequestModel request, string username, HealthRecorderExportModel response)
        {
            string recordName = request.RecordName;
            string categoryName = request.CategoryName;
            string recordNumber = "record" + request.RecordNumber.ToString();
            try
            {
                string query = "select("+recordNumber+") from HealthRecorder where username = @username AND recordName = @recordName AND categoryName = @categoryName";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    var getRecord = conn.QueryFirst<string>(query, new {username = username, recordName = recordName, categoryName = categoryName });
                    response.File = getRecord;
                    
                    return response;
                }

            }
            catch(SqlException ex)
            {
                response.ErrorMessage = ex.Number.ToString();
                return response;
            }
        }
        public HealthRecorderResponseModel EditRecord(HealthRecorderResponseModel response, string username, string newRecordName, string oldRecordName, string categoryName, string record, string secondRecord)
        {
            try
            {
                //incorrect syntax somehwere
                string query = "update HealthRecorder set categoryName = @categoryName, recordName = @newRecordName, record1 = @record, record2 = @secondRecord, timeSaved = CURRENT_TIMESTAMP " +
                    "where username = @username AND recordName = @oldRecordName";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    int result = conn.Execute(query, new { categoryName = categoryName, newRecordName = newRecordName, record = record, secondRecord = secondRecord, username = username, oldRecordName = oldRecordName });
                    if (result == 1)
                    {
                        response.Data = "saved";
                    }
                    return response;
                }

            }
            catch (SqlException ex)
            {
                response.ErrorMessage = ex.Number.ToString();
                return response;
            }
        }
    }
}
