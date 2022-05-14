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
        // async/await
        public string UsernameExists(string username)
        {
            try
            {
                string query = $"select count(@Username) from Accounts where Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query, new { Username = username });
                    if (count != 0)
                    {
                        return "username exists";
                    }
                    return "username not found";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
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
                    int count = connection.ExecuteScalar<int>(query, new { Username = username });
                    if (count == 1)
                    {
                        return "Admin";
                    }
                    return "Member";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

        public string CheckPassword(string username, string password)
        {
            try
            {
              // password = "Password1!";
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
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

        public bool Create(User user)
        {
            try
            {
                string query = "INSERT ACCOUNTS (Username,Email,Password,FirstName,LastName,IsEnabled,IsAdmin,privOption) " +
                               " values (@Username, @Email, @Password, @FirstName,@LastName, 1,0,0) ";
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
        public string UpdateRecoveryAttempts(string username, DateTime recoveryAttempt)
        {
            try
            {

                string query = "INSERT Recovery(username,email, recoveryAttempt) values(@username,'angelcueva47@gmail.com', @recoveryAttempt)";

                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    int lines = conn.Execute(query, new { Username = username, recoveryAttempt = recoveryAttempt }); ;
                    conn.Close();

                    return lines.ToString();

                }

            }
            catch (SqlException e)
            {
                return e.Number.ToString();
            }
        }



        public User Read(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<User> str = connection.Query<User>($"select * from Accounts where Username = @Username", new { Username = user.Username });
                    return str.First();
                }
            }
            catch (Exception)
            {

                return new User("100", "100", "100", "100", "100", 100, 100, 100);
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
                    int linesEdited = connection.Execute(query, new { Username = user.Username });
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

        //TODO:CHANGE TO ERRORS
        public string DeletePastOTP(string username, string codetype)
        {
            try
            {
                string query = "DELETE from VerifyCodes where Username = @Username AND  CodeType = @codetype";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query, new { Username = username, CodeType = codetype });
                    return lines.ToString() + " deleted";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }

        }

        public string SaveActivationCode(string username, DateTime codeDate, string code, string codeType)
        {
            try
            {
                string query = "INSERT INTO VerifyCodes (username, codeDate, code, codetype) " +
                    "values(@username, @codeDate, @code, @codeType)";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query,
                    new
                    {
                        Username = username,
                        codeDate = codeDate,
                        code = code,
                        codetype = codeType
                    }
                    );
                    return "Saved";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }

        }

        public string ValidateOTP(string username, string code)
        {

            try
            {
                string query = "select count(username) from VerifyCodes where username = @Username " +
                "AND code = @Code ";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.ExecuteScalar<int>(query, new { Username = username, Code = code });

                    return lines.ToString();
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
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
                    int attempts = connection.ExecuteScalar<int>(query, new { Username = username });
                    return attempts.ToString();
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString() + "Z";
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
                    int lines = connection.Execute(query, new { Username = username, @Date_Time = DateTime.UtcNow });
                    return lines.ToString();
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString() + "Z";
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
                    int lines = connection.Execute(query, new { Username = username, Attempts = updatedValue });
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
                    IEnumerable<DateTime> dt = connection.Query<DateTime>(query, new { Username = username });
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
                    int lines = connection.Execute(query, new { Username = username, IsEnabled = updateValue });
                    return lines.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public string UnactivatedSave(User user)
        {
            try
            {
                string query = "INSERT into ACCOUNTS(Username,Email,Password,FirstName,LastName,IsEnabled,IsAdmin,privOption) " +
                                " values (@Username, @Email, @Password, @FirstName,@LastName, 0,0, @privOption) ";
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
                            privOption = user.privOption
                        });
                    connection.Close();

                }


                return "Saved";
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }


        }
        public string GetTime(string code, string username)
        {
            try
            {
                string query = "Select CodeDate FROM VerifyCodes WHERE Username = @Username and code =@code ";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<DateTime> timeinDB = connection.Query<DateTime>(query, new
                    {
                        Username = username,
                        code = code
                    }
                    );
                    return timeinDB.First().ToString();
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }
        public string DeleteCode(string username, string codeType)
        {
            try
            {
                string query = "Delete FROM VerifyCodes WHERE Username = @Username and codeType =@codeType ";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query, new
                    {
                        Username = username,
                        codeType = codeType
                    }
                    );
                    return "Deleted";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }
        public string getCode(string username, string codeType)
        {

            try
            {
                string query = "Select code FROM VerifyCodes WHERE Username = @username and codeType =@codetype ";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    var lines = connection.Query<string>(query, new
                    {
                        username = username,
                        codetype = codeType
                    }
                    );
                    return lines.First().ToString();
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

        public string DeleteUnActivated(User user)
        {
            try
            {
                string query = $"DELETE FROM Accounts WHERE Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int linesEdited = connection.Execute(query, new { Username = user.Username });
                    connection.Close();

                    connection.Close();
                    return "1";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
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
                        int lines = connection.Execute(query, new { Username = username });
                        return lines.ToString();
                    }
                }
                catch (SqlException ex)
                {
                    return ex.Number.ToString();
                }
            }
        }
        public string DeleteAccount(string username)
        {
            try
            {
                string query = @"DELETE FROM Accounts WHERE Username = @Username;
                                DELETE FROM diet WHERE Username = @Username;
                                DELETE FROM nutritionanalysis WHERE Username =@Username;
                                DELETE FROM favoriterecipe WHERE Username = @Username;
                                DELETE FROM WMGoals WHERE Username = @Username;
                                DELETE FROM favoriteMedication WHERE Username = @Username;
                                DELETE FROM failedAttempts WHERE Username = @Username;
                                DELETE FROM foodlog WHERE Username = @Username; 
                                DELETE FROM healthrecorder WHERE Username = @Username;
                                DELETE FROM recovery WHERE Username = @Username;
                                DELETE FROM reminders WHERE Username = @Username; 
                                DELETE FROM verifycodes WHERE Username = @Username;
                                DELETE FROM weightgoalimages WHERE Username = @Username;";




                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int linesEdited = connection.Execute(query, new { Username = username });
                    connection.Close();
                    if (linesEdited == 0)
                    {
                        return linesEdited.ToString();
                    }
                    connection.Close();
                    return linesEdited.ToString();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public List<searchItem> getSearchCount(string type)
        {
            string query = "select top 5 *  from searchAnalysis where AnalysisType=@type order by occurences desc";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    IEnumerable<searchItem> viewCount = conn.Query<searchItem>(query, new
                    {
                        type = type
                    });
                    return viewCount.ToList();

                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public async Task<List<timeTotal>> AvgTime()
        {

            string query = "select top 5 *  from viewTime order by seconds/occurences desc";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    await conn.OpenAsync();
                    IEnumerable<timeTotal> viewCount = await conn.QueryAsync<timeTotal>(query);
                    return viewCount.ToList();

                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public async Task<List<timeTotal>> BiggestTime()
        {
            string query = "select top 5 viewName,seconds from viewTime order by seconds desc";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    await conn.OpenAsync();
                    IEnumerable<timeTotal> viewCount = await conn.QueryAsync<timeTotal>(query);
                    return viewCount.ToList();

                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public bool MakeView(string view, float time)
        {
            string query = "INSERT viewTime (viewName,occurences,seconds) " +
                                " values (@viewName, 1, @seconds)";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    int viewCount = conn.ExecuteScalar<int>(query, new
                    {
                        viewName = view,
                        seconds = time
                    });
                    return true;

                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }


        public bool AddTime(string view, float time)
        {
            try
            {
                string query = $"update viewTime set seconds=seconds+@time, occurences=occurences+1 where viewName = @viewName";
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    int viewCount = conn.ExecuteScalar<int>(query, new {
                        viewName = view,
                        time = time
                    });
                    return true;

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public int ViewExists(string view)
        {
            try
            {

                string query = $"select Count(*) from viewTime where viewName = @viewName";
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    int viewCount = conn.ExecuteScalar<int>(query, new { viewName = view });
                    return viewCount;

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public string AcceptEULA(string username)
        {
            try
            {
                string query = $"update Accounts  set privOption = 1 where username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query, new { Username = username });
                    return "accepted";
                    // return count.ToString();

                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

        public string DeclineEULA(string username)
        {
            try
            {
                string query = $"update Accounts where select count(username) from Accounts where username = '{username}' set privOption = False;";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query);
                    return "declined";
                    // return count.ToString();

                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
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
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

        public string ValidateRecoveryAttempts(string username)
        {
            try
            {
                string query = "select count(*) from Recovery where username = @username";
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    int recoveryAttempts = conn.ExecuteScalar<int>(query, new { username = username });

                    if (recoveryAttempts < 5)
                    {
                        return "under";
                    }
                    return "over";

                    //return recoveryAttempts.ToString();
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }


        }

        public string VerifySameDay(string username, string code)
        {
            try
            {
                string query = "select count(CodeDate) from VerifyCodes where Username = @Username and Code = @Code and CodeDate >= DATEADD(day, -1, GETDATE())";
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    int time = conn.ExecuteScalar<int>(query, new { Username = username, Code = code });
                    if (time == 1)
                    {
                        return "same day";
                    }
                    return "Expired Link";
                }
            }
            catch (SqlException e)
            {
                return e.Number.ToString();
            }
        }


        public string ResetPassword(string password, string username)
        {
            try
            {
                string query = "update Accounts set Password = @Password where Username = @Username";
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    int reset = conn.Execute(query, new { Password = password, Username = username });
                    return reset.ToString();
                }

            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }

        }

        public string RemoveRecoveryAttempts(string username)
        {
            try
            {
                string query = "update Recovery set RecoveryAttempts = RecoveryAttempts - 1 where Username =  @Username";

                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    int lines = conn.Execute(query, new { Username = username });
                    return lines.ToString();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string GetRecoveryOTP(string username)
        {
            try
            {
                string query = "select code from VerifyCodes where username = @username and codeType = 'Recovery'";
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    string otp = conn.ExecuteScalar<string>(query, new { Username = username });
                    return otp;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string GetPassword(string username)
        {
            try
            {
                string query = "select password from Accounts where username = @username";
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    string p = conn.ExecuteScalar<string>(query, new { Username = username });
                    return p;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string VerifyTwoMins(string username, string code) {
            try
            {

                DateTime dt = DateTime.UtcNow;
                dt = dt.AddMinutes(-2);
                string query = "select count(username) from VerifyCodes where username = @Username " +
                               "AND code = @Code AND CodeDate > @Dt";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.ExecuteScalar<int>(query, new { Username = username, Code = code, Dt = dt });
                    return lines.ToString();
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

        public string MakeNewUser(string permUsername, string oldUsername)
            {
            try
            {
                string query = "UPDATE Accounts SET IsEnabled = 1, Username=@permUsername WHERE Username = @oldUsername";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query, new
                    {
                        oldUsername = oldUsername,
                        permUsername = permUsername
                    });
                }

                return "activated";
            }

            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }
        public string ActivateUser(string username)
        {
            try
            {
                string query = "UPDATE Accounts SET IsEnabled = 1 WHERE Username = @Username";

                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int lines = connection.Execute(query, new
                    {
                        Username = username
                    });
                }

                return "activated";
            }

            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }

        }
        public List<Tracking> GetLogin(string Type, int months)
        {

            months *= -1;
            string query = " SELECT* FROM TrackerLogs WHERE dateTime >= DATEADD(MONTH, @months, GETDATE()) and logType = @Type";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    IEnumerable<Tracking> viewCount = conn.Query<Tracking>(query, new
                    {
                        Type = Type,
                        months = months
                    });
                    return viewCount.ToList();

                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }




        }
        public List<Tracking> GetReg() 
        {
            string query = " SELECT* FROM TrackerLogs WHERE dateTime >= DATEADD(MONTH, -3, GETDATE()) and logType = 'Registration'";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectString))
                {
                    conn.Open();
                    IEnumerable<Tracking> viewCount = conn.Query<Tracking>(query);
                    return viewCount.ToList();

                }
            }

            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }

        }







    }
}


