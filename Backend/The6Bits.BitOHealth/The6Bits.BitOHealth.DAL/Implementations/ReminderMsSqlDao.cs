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
        public string _connectString;

        public ReminderMsSqlDao(string connectstring)
        {
            _connectString = connectstring;
        }
        public string CreateReminder(string username, string name, string description, string date, string time, string repeat)
        {

            //insert into table
            try
            {
                //change values
                String query = $"INSERT INTO Reminders (username, name, description, date, time, repeat) values ('{username}', '{name}', '{description}', '{date}', '{time}', '{repeat}')";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 1)
                    {
                        return "Reminder Created";
                    }
                    return "Reminder NOT Created";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }

        }
    }
}
