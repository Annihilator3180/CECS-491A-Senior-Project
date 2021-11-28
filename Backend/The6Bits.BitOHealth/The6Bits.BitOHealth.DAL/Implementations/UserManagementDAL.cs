using System;
using System.Data.SqlClient;
using Dapper;
using The6Bits.BitOHealth.Models;



namespace The6Bits.BitOHealth.DAL.Implementations
{
public class UserManagementDAL<T> : IRepository<User>
    {
        public User Read()
        {
            throw new NotImplementedException();
        }

        public bool Create(User user)
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

                return true;
            }
            catch
            {
                return false;
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
