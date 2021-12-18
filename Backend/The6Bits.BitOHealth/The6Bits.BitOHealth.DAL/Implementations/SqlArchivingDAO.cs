using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.Logging.Models;
using The6Bits.BitOHealth.DAL.Contract;
using System.Data.SqlClient;
using Dapper;


namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class SqlArchivingDAO : IArchivingDatabase
    {

        public string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        public IList<string> GetLogsOlderThan30Days(DateTime datetime)
        {
            string query = $"select * from Logs WHERE datediff(day, Date_Time, GETDATE() )>30;";
            IList<string> logList = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    SqlCommand command = new SqlCommand(query,connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    IEnumerable<Log> logs = connection.Query<Log>(query);
                    foreach (Log l in logs)
                    {
                        logList.Add($" {l.username}, {l.description}, {l.LogLevel}, {l.LogCategory}, {l.Date_Time} ");
                    }

                    connection.Close();
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
            string query = $"DELETE FROM Logs WHERE datediff(day, Date_Time, GETDATE() )>30;";
            try
            {
                using (var connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    connection.Execute(query);
                    connection.Close();
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
