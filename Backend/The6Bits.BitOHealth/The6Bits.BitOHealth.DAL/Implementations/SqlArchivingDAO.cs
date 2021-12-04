using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.Logging.Models;
using Dapper;
using The6Bits.BitOHealth.DAL.Contract;
using System.Data.SqlClient;


namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class SqlArchivingDAO : IArchivingDatabase
    {

        private readonly string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        public IList<string> GetLogsOlderThan30Days(DateTime datetime)
        {
            string query = $"select * from LogsTest WHERE datediff(day, Date_Time, GETDATE() )>30;";
            IList<string> logList = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    SqlCommand command = new SqlCommand(query,connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        logList.Add($" {reader["Username"]}, {reader["description"]}, {reader["LogLevel"]}, {reader["LogCategory"]}, {reader["Date_Time"]} ");
                        System.Diagnostics.Debug.WriteLine("\t{0}\t{1}\t{2}",
                            reader["Username"], reader["description"], reader["LogLevel"]);
                    }
                    //reader.Close();
                    //IEnumerable<Log> str = connection.Query<Log>(query);
                    //foreach (Log log in str)
                    //{
                    //    logList.Add( $" {log.username}, {log.description}, {log.LogLevel}, {log.LogCategory}, {log.Date_Time} ");
                    //}

                    return logList;

                }
            }
            catch
            {
                return logList;
            }
        }

        public bool Delete(DateTime datetime)
        {
            try
            {
                string query = $"DELETE FROM LogsTest WHERE datediff(day, Date_Time, GETDATE() )>30;";
                using (var connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    connection.Execute(query);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
