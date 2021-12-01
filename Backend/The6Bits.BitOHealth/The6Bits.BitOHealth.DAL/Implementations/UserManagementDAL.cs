using System;
using System.Data.SqlClient;
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

        public User Read(string username)
        {
            try
            {
                using (var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    connection.Open();
                    User u = new User();
                    //TODO : ERROR CHECK EVERYTHING HERE
                    u.Email = connection.ExecuteScalar<string>($"SELECT username FROM Accounts WHERE email = '{username}'; ");
                    return u;
                }
            }
            catch { 
      
                return new User();
            }
        
        }



        public bool Update(User user)
        {
            return true;
        }

        public bool Delete(User user)
        {
            return true;
        }
        

    }
}
