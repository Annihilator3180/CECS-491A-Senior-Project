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
        public string UsernameAndEmailExists(string username, string email)
        {
            try
            {
                string query = $"select Count(*) from Accounts where Username = '{username}' AND Email = '{email}';";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query);
                    if (count == 1)
                    {
                        return "Email and Username found";
                    }
                    return "incorrect";
                }

            }
            catch
            {
                return "Db error";
            }
        }
        public string IsEnabled(string username)
        {
            try
            {
                string query = $"select isenabled from Accounts where username = '{username}';";
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    int isEnabled = conn.ExecuteScalar<int>(query);
                    if (isEnabled == 1)
                    {
                        return "isEnabled";
                    }
                    return "isDisabled";
        
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
                return "DB Error";
            }
        }
                return false;
            }
        }

        //TODO:CHANGE TO ERRORS
        public string DeletePastOTP(string username, string codetype)
        {
            try
            {
                string query = "DELETE from VerifyCodes where Username = @Username AND  CodeType = @codetype";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query, new{ Username = username, CodeType=  codetype});
                    return lines.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        
        public string SaveActivationCode( string username , DateTime time, string code, string codeType)
        {
            try
            {
                string query = "INSERT VerifyCodes(username,time,code,codeType) VALUES (@username,@time,@code,@codetype) ";
                
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query, new{ Username = username, time = time, code = code , codetype =  codeType});
                    return lines.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public string ValidateOTP(string username, string code)
        {
            
            try
            {
                string query = "select count(username) from VerifyCodes where username = @Username AND code = @Code ";
                
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.ExecuteScalar<int>(query, new{ Username = username, Code = code });

                    return lines.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public string CheckFailedAttempts(string username)
        {
            try
            {
                string query = "SELECT Attempts FROM FailedAttempts WHERE Username = @Username";
                
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int attempts = connection.ExecuteScalar<int>(query, new{ Username = username});
                    return attempts.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string InsertFailedAttempts(string username)
        {
            try
            {
                string query = "INSERT FailedAttempts(Username, Attempts,Date_Time) VALUES (@Username,1,@Date_Time)";
                
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.ExecuteScalar<int>(query, new{ Username = username, @Date_Time = DateTime.UtcNow});
                    return lines.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public string UpdateFailedAttempts(string username, int updatedValue)
        {
            try
            {
                string query = "UPDATE FailedAttempts SET Attempts = @Attempts WHERE Username = @Username";
                
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.ExecuteScalar<int>(query, new{ Username = username, Attempts = updatedValue});
                    return lines.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        
        public string CheckFailDate(string username)
        {
            try
            {
                string query = "SELECT Date_Time FROM FailedAttempts WHERE Username = @Username";
                
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<DateTime> dt = connection.Query<DateTime>(query, new{ Username = username});
                    if (dt == null || !dt.Any())
                    {
                        return "none";
                    }
                    return dt.First().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string UpdateIsEnabled(string username, int updateValue)
        {
            try
            {
                string query = "UPDATE Accounts SET IsEnabled = @IsEnabled WHERE Username = @Username";
                
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query, new{ Username = username, IsEnabled = updateValue});
                    return lines.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteFailedAttempts(string username)
        {
            {
                try
                {
                    string query = "DELETE FROM FailedAttempts WHERE Username = @Username ";
                
                    using (SqlConnection connection = new SqlConnection(_connectString))
                    {
                        connection.Open();
                        int lines = connection.Execute(query, new{ Username = username});
                        return lines.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            
        }









    }
}
