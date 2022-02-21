using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using Dapper;
using The6Bits.BitOHealth.Models;


namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class AccountMsSqlDao : IRepositoryAuth<string>
    {
        private string _connectString;

        

        public AccountMsSqlDao(string connectstring)
        { 
            _connectString = connectstring;
        }

        public string UsernameExists(string username)
        {
            try
            {
                string query = $"select count(@Username) from Accounts where Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query, new {Username = username});
                    if (count != 0)
                    {
                        return "username exists";
                    }
                    return "username not found";
                }
            }
            catch
            {
                return "DB Error Username Exists";
            }


        }
        public string UserRole(string username)
        {
            try
            {
                string query = $"select IsAdmin from Accounts where Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query,new {Username = username});
                    if (count == 1)
                    {
                        return "Admin";
                    }
                    return "Member";
                }
            }
            catch
            {
                return "DB Error";
            }
        }

        public string CheckPassword(string username, string password)
        {
            try
            {
                string query = "select Count(@Username) from Accounts where Username = @Username AND Password = @Password";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    var res = connection.ExecuteScalar<int>(query,
                        new
                        {
                            Username = (string)username,
                            Password = (string)password
                        });
                    if (res == 1)
                    {
                        return "credentials found";
                    }
                    return "not found";
                }
            }
            catch
            {
                return "DB Error Check Pass";
            }
        }
        
        public bool Create(User user)
        {
            try
            {
                string query = "INSERT ACCOUNTS(Username,Email,Password,FirstName,LastName,IsEnabled,IsAdmin) " +
                               " values (@Username, @Email, @Password, @FirstName,@LastName, 0,0) ";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    int lines_modified = connection.Execute(query,
                        new
                        {
                            Email = user.Email,
                            Username = user.Username,
                            Password = user.Password,
                            FirstName = user.FirstName,
                            LastName = user.LastName
                        });
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
        
        public User Read(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<User> str = connection.Query<User>($"select * from Accounts where Username = @Username", new{ Username = user.Username});
                    return str.First();
                }
            }
            catch
            {
                return new User();
            }
        
        }
        
        
        public bool Delete(User user)
        {
            try
            {
                string query = $"DELETE FROM Accounts WHERE Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int linesEdited = connection.Execute(query, new {Username = user.Username});
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
        
        
        
        
        
        
    }
}
