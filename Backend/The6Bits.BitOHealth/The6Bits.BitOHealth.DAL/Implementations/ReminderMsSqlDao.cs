using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class ReminderMsSqlDao : IReminderDatabase
    {
        public string _connectString;

        public ReminderMsSqlDao(string connectstring)
        {
            _connectString = connectstring;
        }

        public string DBErrorCheck(int ErrorNumber)
        {
            if (ErrorNumber == -2)
            {
                return "Database Time Out Error";
            }
            else if (ErrorNumber == 1105)
            {
                return "Database Full";
            }
            else if (ErrorNumber == 4060)
            {
                return "Database Offline";
            }
            else if (ErrorNumber == 2627)
            {
                return "Duplicate Record Name";
            }
            else
            {
                return ErrorNumber.ToString() + "Database Other Error ";
            }
        }

        public async Task<string> CreateReminder(int count, string username, string name, string description, string date, string time, string repeat)
        {
            //FIX ME: if description has '.' leave alone, else add '.' to end
            //description = description + ".";
            //insert into table
            try
            {
                //change values
                String query = $"INSERT INTO Reminders (R_SK, username, name, description, date, time, repeat) values ('{count}', '{username}', '{name}', '{description}', '{date}', '{time}', '{repeat}')";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = await connection.ExecuteAsync(query);
                    if (s == 1)
                    {
                        return "Reminder Created";
                    }
                    return "Reminder NOT Created";
                }
            }
            catch (SqlException ex)
            {
                return DBErrorCheck(ex.Number);
            }

        }

        public async Task<string> ViewAllReminders(string username)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<ReminderModel> str = connection.Query<ReminderModel>($"select * from Reminders where username = '{username}';");
                    string s = "";
                    foreach (ReminderModel remindermodel in str)
                    {
                        s += $"{remindermodel.name}{":"} {remindermodel.description}";

                    }

                    return s;

                }
            }
            catch (SqlException ex)
            {
                return DBErrorCheck(ex.Number);
            }
        }

        public async Task<string> ViewAllHelper(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<ReminderModel> str = connection.Query<ReminderModel>($"select * from Reminders where username = '{username}';");
                    string s = "";
                    foreach (ReminderModel remindermodel in str)
                    {
                        s += $"{remindermodel.name}{":"} {remindermodel.description}";

                    }

                    return s;

                }
            }
            catch (SqlException ex)
            {
                return DBErrorCheck(ex.Number);
            }
        }

        public async Task<string> ViewHelper(string username, string reminderID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<ReminderModel> str = connection.Query<ReminderModel>($"select * from Reminders where username = '{username}' AND R_SK = '{reminderID}';");
                    string s = "";
                    foreach (ReminderModel remindermodel in str)
                    {
                        s += $"{remindermodel.name} {remindermodel.description} {remindermodel.date} {remindermodel.time} {remindermodel.repeat}";

                    }

                    return s;

                }
            }
            catch (SqlException ex)
            {
                return DBErrorCheck(ex.Number);
            }
        }

        public List<string> EditHelper(string username, string reminderID)
        {
            string aR_SK = "", aname = "", adescription = "", adate = "", atime = "", arepeat = "";
            using (SqlConnection connection = new SqlConnection(_connectString))
            {
                connection.Open();
                IEnumerable<ReminderModel> str = connection.Query<ReminderModel>($"select * from Reminders where username = '{username}';");
                string s = "";
                foreach (ReminderModel remindermodel in str)
                {

                    aR_SK = remindermodel.R_SK; aname = remindermodel.name; adescription = remindermodel.description; adate = remindermodel.date; atime = remindermodel.time; arepeat = remindermodel.repeat;
                }
            }
            List<string> edit = new List<string> { aname, adescription, adate, atime, arepeat };

            return edit;

        }

        public async Task<string> DeleteReminder(string username, string reminderID)
        {
            try
            {
                //Delete Reminder
                String query = $"DELETE FROM Reminders WHERE R_SK = '{reminderID}' AND username = '{username}'";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s == 1)
                    {
                        return "Reminder Deleted";
                    }
                    return "Reminder NOT Deleted";
                }
            }
            catch (SqlException ex)
            {
                return DBErrorCheck(ex.Number);
            }
        }

        public async Task<string> EditReminder(string username, string reminderID, string name, string description, string date, string time, string repeat)
        {
            try
            {
                //Edit Reminder
                String query = $"Update Reminders SET name = '{name}', description = '{description}', date = '{date}', time = '{time}', " +
                    $"repeat = '{repeat}' WHERE R_SK = '{reminderID}' AND username = '{username}'";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int s = connection.Execute(query);
                    if (s != 0)
                    {
                        return "Reminder Edited";
                    }
                    return "Reminder NOT Edited";
                }
            }
            catch (SqlException ex)
            {
                return DBErrorCheck(ex.Number);
            }
        }
        public async Task<int> GetCount(string username)
        {
            int res = 0;
            try
            {

                string query = $"SELECT count(username) FROM Reminders WHERE username = '{username}';";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    res = connection.ExecuteScalar<int>(query);
                    return (res + 1);
                }
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
        }

    }
}
