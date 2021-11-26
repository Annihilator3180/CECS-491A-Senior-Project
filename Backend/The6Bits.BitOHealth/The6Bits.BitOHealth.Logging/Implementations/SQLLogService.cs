using System;
using System.Data.SqlClient;
using Dapper;

namespace The6Bits.Logging.Implementations
{
    public class SQLLogService : ILogService
    {

        public string getAllLogs()
        {
            throw new NotImplementedException();
        }

        public bool Log(string username, string description, Enum LogLevel, Enum LogCategory)
        {
            try
            {
                using (var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    connection.Open();
                    string addLog = $"INSERT INTO LogsTest (username, description, LogLevel, LogCategory, Date_Time) values ('{username}', '{description}', '{LogLevel}' , '{LogCategory}', CURRENT_TIMESTAMP)";
                    var res = connection.ExecuteScalar<string>(addLog);
                    Console.WriteLine(res);
                    connection.CloseAsync();
                }
                Console.WriteLine();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
