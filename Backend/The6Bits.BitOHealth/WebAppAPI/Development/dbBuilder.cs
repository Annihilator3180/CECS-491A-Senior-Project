using System.Data.SqlClient;

namespace WebAppMVC.Development
{
    public class dbBuilder
    {

        public bool buildDB(string connStr)
        {
            var AccountsStr = "If not exists (select name from sysobjects where name = 'Accounts') CREATE TABLE Accounts ( Username VARCHAR(30) NOT NULL, Email VARCHAR(255), Password VARCHAR(30),FirstName VARCHAR(20),LastName VARCHAR(20),IsEnabled BIT, IsAdmin BIT )";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(AccountsStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;
        }
    }
}
