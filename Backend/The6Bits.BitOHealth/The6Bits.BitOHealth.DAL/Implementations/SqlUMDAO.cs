using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using The6Bits.BitOHealth.Models;



namespace The6Bits.BitOHealth.DAL.Implementations
{
public class SqlUMDAO<T> : IRepositoryUM<User>
    {

        private readonly string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        //TODO:Rename error, fix SQL handling
        public bool Create(User user)
        {
            try
            {
                string query = InsertQueryBuilder(user);
                using var connection = new SqlConnection(_connectString);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteReader();
                //connection.ExecuteScalar<string>(query);
                //Console.WriteLine(bruh);
                connection.Close();


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
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        user.Username = reader["Username"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["Password"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();

                        user.IsEnabled = Convert.ToInt32(reader["IsEnabled"]);
                        user.IsAdmin = Convert.ToInt32(reader["IsAdmin"]);


                        reader.Close();
                        connection.Close();
                        return user;

                    }
                    reader.Close();
                    connection.Close();
                    return new User();


                    //IEnumerable<User> str = connection.Query<User>($"select * from Accounts where username = '{user.Username}'; ");
                }
            }
            catch
            {
                return new User();
            }
        
        }


        //TODO: ASK better for generic update or specific methods for each change? which for SOLID
        //ex: DAL.UpdateEmail();
        public bool Update(User user)
        {
            try
            {
                string query = UpdateQueryBuilder(user);
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
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
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteReader();

                    connection.Close();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        //Mayde add Enable/Disable account 
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

        public string InsertQueryBuilder(User user)
        {
            string query = "INSERT INTO Accounts (";
            List<string> varnames = new List<string> { };
            List<string> values = new List<string> { };
            foreach (var prop in user.GetType().GetProperties())
            {
                if (prop.GetValue(user, null) != null && prop.PropertyType == typeof(string))
                {
                    varnames.Add($" {prop.Name} ");
                    values.Add($"'{prop.GetValue(user, null)}'");
                }
                if (prop.GetValue(user, null) != null && prop.PropertyType != typeof(string))
                {
                    varnames.Add($" {prop.Name} ");
                    values.Add($"{prop.GetValue(user, null)}");
                }
            }
            query += $"{string.Join(", ", varnames)}) VALUES ( ";
            query += $"{string.Join(", ", values)});";
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
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();

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
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteReader();
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
