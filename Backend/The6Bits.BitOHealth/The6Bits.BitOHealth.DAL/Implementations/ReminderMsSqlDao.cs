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
        public int GetCount(string username)
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
            catch
            {
                return res;
            }
        }
        public string CreateReminder(int count, string username, string name, string description, string date, string time, string repeat)
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

        public string ViewAllReminders(string username)
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
            catch
            {
                return "";
            }
        }

        public string ViewAllHelper(string username)
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
            catch
            {
                return "";
            }
        }

        public string ViewHelper(string username)
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
                        s += $"{remindermodel.name} {remindermodel.description} {remindermodel.date} {remindermodel.time} {remindermodel.repeat}{"ENDING"}";

                    }

                    return s;

                }
            }
            catch
            {
                return "";
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

        public string DeleteReminder(string username, string reminderID)
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
                return ex.Number.ToString();
            }
        }

        public string EditReminder(string username, string reminderID, string name, string description, string date, string time, string repeat)
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
                    if (s == 1)
                    {
                        return "Reminder Edited";
                    }
                    return "Reminder NOT Edited";
                }
            }
            catch
            {
                return "Edit failed";
            }
        }

    }
}
