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

        public string getAllTrackerLogs()
        {
            string query = $"select * from trackerLogs ;";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {


                    connection.Open();
                    IEnumerable<TrackerLog> str = connection.Query<TrackerLog>($"select * from trackerLogs ;");
                    string s = "";
                    foreach (TrackerLog trackerlog in str)
                    {
                        s += $" {trackerlog.count} {trackerlog.dateTime} {trackerlog.logType}";

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

        public bool RegistrationChecker(string username, string description, string LogLevel, string LogCategory)
        {
            //select date count, check if table has instance for given date
            try
            {
                string date = DateTime.UtcNow.ToString("MM-dd-yyyy");

                string query = $"SELECT count(*) FROM TrackerLogs WHERE dateTime = '{date}' AND logType = 'Registration'";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool RegistrationInsert(string username, string description, string LogLevel, string LogCategory)
        {
            //update table and add plus one to given date
            try
            {
                String date = DateTime.UtcNow.ToString("MM-dd-yyyy");

                String query = $"INSERT INTO TrackerLogs (count, dateTime, logType) values ('{1}', '{date}', 'Registration')";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool RegistrationLog(string username, string description, string LogLevel, string LogCategory)
        {
            //insert to table new row for given date
            try
            {
                String date = DateTime.UtcNow.ToString("MM-dd-yyyy");

                String query = $"UPDATE TrackerLogs SET count = count + 1 WHERE dateTime = '{date}' AND logType = 'Registration'";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool LoginChecker(string username, string description, string LogLevel, string LogCategory)
        {
            //select date count, check if table has instance for given date
            try
            {
                string date = DateTime.UtcNow.ToString("MM-dd-yyyy");

                string query = $"SELECT count(*) FROM TrackerLogs WHERE dateTime = '{date}' AND logType = 'Login';";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool LoginInsert(string username, string description, string LogLevel, string LogCategory)
        {
            //update table and add plus one to given date
            try
            {
                String date = DateTime.UtcNow.ToString("MM-dd-yyyy");

                String query = $"INSERT INTO TrackerLogs (count, dateTime, logType) values (1, '{date}', 'Login');";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool LoginLog(string username, string description, string LogLevel, string LogCategory)
        {
            //insert to table new row for given date
            try
            {
                String date = DateTime.UtcNow.ToString("MM-dd-yyyy");

                String query = $"UPDATE TrackerLogs SET count = count + 1 WHERE dateTime = '{date}' AND logType = 'Login'";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }


    }
}
