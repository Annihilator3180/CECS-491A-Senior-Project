using System;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using Dapper;
using The6Bits.BitOHealth.Models;



namespace The6Bits.BitOHealth.DAL.Implementations
{
public class UserManagementDAL<T> : IRepository<User>
    {


        //TODO:Rename error, fix SQL handling
        public string Create(User user)
        {
            try
            {
                using (var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    connection.Open();
                    string addUser = $"INSERT INTO Accounts (username, email, password, first_name, last_name, enabled, administrator) values('{user.Username}', '{user.Email}', '{user.Password}', '{user.FirstName}', '{user.LastName}', {user.IsEnabled}, {user.IsAdmin}); ";
                    var bruh = connection.ExecuteScalar<string>(addUser);
                    //Console.WriteLine(bruh);
                    connection.CloseAsync();
                }

                return "account created";
            }
            catch
            {
                return "error";
            }
            
        }

        //TODO:Rename error, fix SQL handling

        public User Read(User user)
        {
            try
            {
                using (var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    connection.Open();
                    //TODO : ERROR CHECK EVERYTHING HERE
                    var email = connection.ExecuteScalar<string>($"SELECT username FROM Accounts WHERE username = '{user.Username}'; ");
                    User u = new User(email);
                    return u;
                }
            }
            catch
            {
                return new User("");
            }
        
        }



        public bool Update(User user)
        {
            try
            {
                using (var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    connection.Open();
                    //TODO : ERROR CHECK EVERYTHING HERE
                    if (user.Email != null)
                    {
                        connection.ExecuteScalar<string>($"UPDATE Accounts SET email='{user.Email}' WHERE username = '{user.Username}'; ");
                    }
                    if (user.Password != null)
                    {
                        connection.ExecuteScalar<string>($"UPDATE Accounts SET password='{user.Password}' WHERE username = '{user.Username}'; ");
                    }
                    if (user.FirstName != null)
                    {
                        connection.ExecuteScalar<string>($"UPDATE Accounts SET first_name='{user.FirstName}' WHERE username = '{user.Username}'; ");

                    }

                    if (user.LastName != null)
                    {
                        connection.ExecuteScalar<string>($"UPDATE Accounts SET first_name='{user.FirstName}' WHERE username = '{user.Username}'; ");
                    }

                        var email = connection.ExecuteScalar<string>($"UPDATE Accounts SET email={user.Email}, password = {user.Password}, WHERE username = '{user.Username}'; ");
                    if (user.Email != null)
                    {
                        
                    }
                        User u = new User(email);
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
            return true;
        }
        

    }
}
