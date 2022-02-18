using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using Dapper;


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
                string query = $"select count(*) from Accounts where Username = '{username}';";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query);
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
                string query = $"select IsAdmin from Accounts where Username = '{username}';";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query);
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
                string query = $"select Count(*) from Accounts where Username = '{username}' AND Password = '{password}';";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.ExecuteScalar<int>(query);
                    if (count == 1)
                    {
                        return "credentials found";
                    }
                    return "incorrect";
                }
            }
            catch
            {
                return "DB Error Check Pass";
            }
        }
    }
}
