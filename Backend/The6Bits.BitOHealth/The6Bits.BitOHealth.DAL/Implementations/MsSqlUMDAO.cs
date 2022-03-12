using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using The6Bits.BitOHealth.Models;
using Dapper;



namespace The6Bits.BitOHealth.DAL.Implementations
{
public class MsSqlUMDAO<T> : IRepositoryUM<User>
{

    private string _connectString;


        public MsSqlUMDAO()
        {
        }

        public MsSqlUMDAO(string connectstring)
        {
            _connectString = connectstring;
        }

        //TODO:Rename error, fix SQL handling
        public bool Create(User user)
        {
            try
            {
                string query = "INSERT ACCOUNTS (Username,Email,Password,FirstName,LastName,IsEnabled,IsAdmin,privOption) " +
                               " values (@Username, @Email, @Password, @FirstName,@LastName, @IsEnabled,@IsAdmin,@privOption) ";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    int lines_modified = connection.Execute(query,
                        new
                        {
                            Email = user.Email,
                            Username = user.Username,
                            Password = user.Password,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            IsEnabled = user.IsEnabled,
                            IsAdmin = user.IsAdmin,
                            privOption = user.privOption
                        }); ;
                    connection.Close();
                    if (lines_modified == 0)
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
        //TODO:Rename error, fix SQL handling
        public User Read(User user)
        {
            string query = $"select * from Accounts where username = '{user.Username}'; ";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<User> str = connection.Query<User>($"select * from Accounts where username = '{user.Username}'; ");
                    return str.First();
                }
            }
            catch
            {
                return new User();
            }
        
        }


        //ex: DAL.UpdateEmail();
        public bool Update(User user)
        {
            try
            {
                string query = UpdateQueryBuilder(user);
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int linesEdited = connection.Execute(query);
                    connection.Close();
                    if (linesEdited == 0)
                    {
                        return false;
                    }
                    return true;

                }
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(User user)
        {
            try
            {
                string query = $"DELETE FROM Accounts WHERE Username = '{user.Username}';";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int linesEdited = connection.Execute(query);

                    connection.Close();
                    if (linesEdited == 0)
                    {
                        return false;
                    }
                    connection.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        public string UpdateQueryBuilder(User user)
        {
            string query = "UPDATE Accounts SET ";
            List<string> f = new List<string> { };
            foreach(var prop in user.GetType().GetProperties())
            {
                if(prop.GetValue(user, null) != null && prop.PropertyType == typeof(string))
                {
                    f.Add($" {prop.Name} = '{prop.GetValue(user, null)}'");
                }
                if (prop.GetValue(user, null) != null && prop.PropertyType != typeof(string))
                {
                    f.Add($" {prop.Name} = {prop.GetValue(user, null)}");
                }
            }
            query += string.Join(", ", f);
            query += $" WHERE username = '{user.Username}';";

            return query;
        }

        
        //TODO:test
        public bool EnableAccount(string username)
        {
            try
            {
                string query = $"UPDATE Accounts SET IsEnabled = 1 WHERE username = '{username}';";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int linesEdited =  connection.Execute(query);
                    connection.Close();
                    if (linesEdited == 0)
                    {
                        return false;
                    }
                    return true;
                }

            }
            catch
            {
                return false;
            }


        }
        public bool DisableAccount(string username)
        {
            try
            {
                string query = $"UPDATE Accounts SET IsEnabled = 0 WHERE Username = '{username}';";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int linesEdited = connection.Execute(query);
                    if (linesEdited == 0)
                    {
                        return false;
                    }
                    connection.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }


        }

        public bool UsernameExists(string username)
        {
            try
            {
                string query = $"select count(*) from Accounts where Username = '{username}';";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query);
                    if (count != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return true;
            }


        }

    }
}
