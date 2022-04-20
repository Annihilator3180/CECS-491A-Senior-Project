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
            var AccountsStr = "If not exists (select name from sysobjects where name = 'VerifyCodes') CREATE TABLE VerifyCodes ( username VARCHAR(30) NOT NULL, " +
                "CodeDate DateTime, code VARCHAR(40), codeType VARCHAR(30))";
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
        
        public bool buildRecovery(string connStr)
        {
            var RecoverysStr = "If not exists (select name from sysobjects where name = 'Recovery') CREATE TABLE Recovery ( Username VARCHAR(30) NOT NULL," +
                " email varchar(100), recoveryAttempt DateTime, primary key (username, recoveryAttempt))";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(RecoverysStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;
        }
        public bool BuildHealthRecorder(string connStr)
        {
            var healthRecorderStr = "If not exists (select name from sysobjects where name = 'HealthRecorder') create table HealthRecorder(record varchar(max)," +
                "username varchar(30),timeSaved DateTime, categoryName varchar(50), recordName varchar(50))";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(healthRecorderStr, conn))
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

        public bool buildWMGoals(string connStr)
        {
            var RecoveryStr = "If not exists (select name from sysobjects where name = 'WMGoals') CREATE TABLE WMGoals (Username VARCHAR(30), GoalWeight int, GoalDate DateTime, exerciseLevel int )";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(RecoveryStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;



        }
        public bool buildFavoriteMedication(string connStr)
        {
            var FavoriteStr = "If not exists (select name from sysobjects where name = 'favoriteMedication') " +
                "CREATE TABLE favoriteMedication (Username VARCHAR(30), product_id VARCHAR(100), " +
                "generic_name VARCHAR(100), brand_name VARCHAR(100), lowestPrice int, lowestPriceLocation VARCHAR(150))";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(FavoriteStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;



        }
        public bool buildDiet(string connStr)
        {
            var RecoveryStr = "If not exists (select name from sysobjects where name = 'Diet')" + "CREATE TABLE Diet (Username VARCHAR(30), Diet VARCHAR(30), Health VARCHAR(30), Ingr Int, DishType VARCHAR(30),Calories int, CuisineType VARCHAR(30), Excluded VARCHAR(30), MealType VARCHAR(30))";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(RecoveryStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;



        }
        public bool buildFavoriteRecipe(string connStr)
        {
            var RecoveryStr = "If not exists (select name from sysobjects where name = 'FavoriteRecipe')" + "CREATE TABLE FavoriteRecipe (username VARCHAR(30), recipe_id VARCHAR(255))";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(RecoveryStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false; 

        }




        public bool buildRemiders(string connStr)
        {
            var RecoveryStr = "If not exists (select name from sysobjects where name = 'Reminders') CREATE TABLE Reminders (R_SK VARCHAR(30), username VARCHAR(30), name VARCHAR(30), description VARCHAR(30), date VARCHAR(30), time VARCHAR(30), repeat VARCHAR(30))";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(RecoveryStr, conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            return false;

        }



        public bool addBossAdmin(string connStr)
        {
            var RecoveryStr = "BEGIN IF NOT EXISTS (Select * From Accounts where Username = 'bossadmin12')  " +
                " BEGIN INSERT  INTO Accounts " +
            "(Username, Email, Password, FirstName, LastName, IsEnabled, IsAdmin, privOption)"+
            "values('bossadmin12', 'boof@kizmoz.com', 'JJsZQkSJ1WeC/t0cw+8w093KvafOvQ9umEwRJhZpvnE=.LNdXDlkAGS', 'admin', 'boss', 1, 1,1) " +
            "END END";
            var conn = new SqlConnection(connStr);
            using (SqlCommand command = new SqlCommand(RecoveryStr, conn))
            {
                conn.Open();
                //command.ExecuteScalar();
            }
            return false;



        }
     


    }
}
