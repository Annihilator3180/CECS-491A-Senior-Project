using System.Data.SqlClient;

namespace WebAppMVC.Development
{
    public class dbBuilder
    {

        public bool builAccountdDB(string connStr)
        {
            var AccountsStr = "If not exists (select name from sysobjects where name = 'Accounts') " +
                "CREATE TABLE Accounts ( Username VARCHAR(30) NOT NULL, Email VARCHAR(255), " +
                "Password VARCHAR(255),FirstName VARCHAR(20),LastName VARCHAR(20),IsEnabled BIT, " +
                "IsAdmin BIT , privOption BIT)";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(AccountsStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;
        }
        
        public bool buildLogDB(string connStr)
        {
            var AccountsStr = "If not exists (select name from sysobjects where name = 'Logs') CREATE TABLE Logs ( username VARCHAR(30) NOT NULL, description VARCHAR(255), LogLevel VARCHAR(30), LogCategory VARCHAR(30), Date_Time DateTime)";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(AccountsStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;
        }
        
        public bool buildVerifyCodes(string connStr)
        {
            var AccountsStr = "If not exists (select name from sysobjects where name = 'VerifyCodes')CREATE TABLE VerifyCodes ( username VARCHAR(30),time DateTime,code VARCHAR(30),codeType VARCHAR(30),primary key(username, codeType)); ";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(AccountsStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;
        }
        
        public bool buildFailedAttempts(string connStr)
        {
            var AccountsStr = "If not exists (select name from sysobjects where name = 'FailedAttempts') CREATE TABLE FailedAttempts ( Username VARCHAR(30) NOT NULL, Attempts int, Date_Time DateTime)";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(AccountsStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;
        }
        public bool buildRecoveryDB(string connStr)
        {
            var RecoveryStr = "If not exists (select name from sysobjects where name = 'Recovery') CREATE TABLE Recovery ( Username VARCHAR(30) NOT NULL, Email VARCHAR(255) NOT NULL, RecoveryAttempts int)";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(RecoveryStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;



        }
        public bool buildTrackerLogs(string connStr)
        {
            var RecoveryStr = "If not exists (select name from sysobjects where name = 'TrackerLogs') CREATE TABLE TrackerLogs ( count int, dateTime VARCHAR(30), logType VARCHAR(30))";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(RecoveryStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;



        }


    }
}
