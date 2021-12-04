using System;
using System.Data.SqlClient;
using System.Text;
using The6Bits.Logging.Models;

namespace The6Bits.Logging.Implementations
{
    public class SQLLogDAO : ILogDal
    {
        private readonly string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        public string getAllLogs()
        {
            string query = $"select * from LogsTest ;";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {


                    string logs = "";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        logs+=($" {reader["username"]}, {reader["description"]}, {reader["LogLevel"]}, {reader["LogCategory"]}, {reader["Date_Time"]} ");
                        System.Diagnostics.Debug.WriteLine("\t{0}\t{1}\t{2}",
                            reader["Username"], reader["description"], reader["LogLevel"]);
                    }



                    return logs;
                    
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
                    string query = $"INSERT INTO LogsTest (username, description, LogLevel, LogCategory, Date_Time) values ('{username}', '{description}', '{LogLevel}' , '{LogCategory}', '{DateTime.UtcNow}')";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
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
