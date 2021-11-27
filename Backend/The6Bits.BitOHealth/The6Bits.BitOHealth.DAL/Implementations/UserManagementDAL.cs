using System;
using System.Data.SqlClient;
using Dapper;
using The6Bits.BitOHealth.Models;



namespace The6Bits.BitOHealth.DAL
{
    public class UserManagementDAL
    {

        public bool CreateAccount(User user)
        {
            try
            {
                using (var connection = new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    connection.Open();
                    string addUser = $"INSERT INTO Accounts (username, email, password, first_name, last_name, enabled, administrator) values('{user.Username}', '{user.Email}', '{user.Password}', '{user.FirstName}', '{user.LastName}', {user.IsEnabled}, {user.IsAdmin}); ";
                    var bruh = connection.ExecuteScalar<string>(addUser);
                    Console.WriteLine(bruh);
                    var accs = connection.ExecuteScalar<string>("SELECT * from Accounts");
                    Console.WriteLine(accs);//
                    connection.CloseAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }

        }

    }
}
