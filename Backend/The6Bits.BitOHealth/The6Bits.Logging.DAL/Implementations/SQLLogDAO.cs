using System;
using System.Data.SqlClient;
using System.Text;
using The6Bits.Logging.DAL.Contracts;
using Dapper;
using The6Bits.Logging.Models;

namespace The6Bits.Logging.DAL.Implementations
{
    public class SQLLogDAO : ILogDal
    {
        public string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        public string getAllLogs()
        {
            string query = $"select * from Logs ;";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {


                    connection.Open();
                    IEnumerable<Log> str = connection.Query<Log>($"select * from Logs ;");
                    string s = "";
                    foreach (Log log in str)
                    {
                        s += $" {log.username} {log.description} {log.LogLevel} {log.LogCategory} {log.Date_Time} ";
                        System.Diagnostics.Debug.WriteLine(log.username + "     " + log.Date_Time);

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
                string query =  $"INSERT INTO Logs (username, description, LogLevel, LogCategory, Date_Time) values ('{username}', '{description}', '{LogLevel}' , '{LogCategory}', '{DateTime.UtcNow}')";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 1) 
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool RegistrationChecker(string username, string description, string LogLevel, string LogCategory)
        {
            //select date count, check if table has instance for given date
            try
            {
                string query = $"INSERT INTO Logs (username, description, LogLevel, LogCategory, Date_Time) values ('{username}', '{description}', '{LogLevel}' , '{LogCategory}', '{DateTime.UtcNow}')";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }

        }

        public bool RegistrationLog(string username, string description, string LogLevel, string LogCategory)
        {
            //update table and add plus one to given date
            return false;
        }

        public bool RegistrationInsert(string username, string description, string LogLevel, string LogCategory)
        {
            //insert to table new row for given date
            return false;
        }


    }
}
