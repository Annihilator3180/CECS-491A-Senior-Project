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
    public class ReminderMsSqlDao : IReminderDatabase
    {
        public string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        public ReminderMsSqlDao(string connectstring)
        {
            _connectString = connectstring;
        }
        public bool CreateReminder(string username, string name, string description, string date, string time, string repeat)
        {

            //insert into table
            try
            {
                //change values
                String query = $"INSERT INTO Reminders (username, name, description, date, time, repeat) values ('{1}', '{date}', 'Registration')";

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
