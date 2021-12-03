using System;
using System.Data.SqlClient;
using Dapper;
using The6Bits.Logging.Models;

namespace The6Bits.Logging.Implementations
{
    public class SQLLogDAO : ILogDal
    {
        private readonly string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        public string getAllLogs()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<Log> str = connection.Query<Log>($"select * from LogsTest ;");
                    string s = "";
                    foreach (Log log in str)
                    {
                        s += $" {log.username} {log.description} {log.LogLevel} {log.LogCategory} {log.DateTime} ";
                    }

                    return s;
                    
                }
            }
            catch
            {
                return "";
            }
        }

        public bool Log(string username, string description, string LogLevel, string LogCategory)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    string addLog = $"INSERT INTO LogsTest (username, description, LogLevel, LogCategory, Date_Time) values ('{username}', '{description}', '{LogLevel}' , '{LogCategory}', '{DateTime.UtcNow}')";
                    int s = connection.Execute(addLog);
                    connection.CloseAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
