using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    internal class ReminderMsSqlDao : IReminderDatabase
    {
        public string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        public bool CreateReminder(string reminderName, string description, string date, string time, string repeat)
        {

            //update table and add plus one to given date
            try
            {
                
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
    }
}
