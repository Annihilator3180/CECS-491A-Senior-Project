using Xunit;
using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Implementations;
using System;

namespace The6Bits.Logging.Test
{
    public class trackerTest
    {

        [Fact]
        public void log()
        {
            LogService log = new LogService(new SQLLogDAO());
            var b = log.Log("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            bool isValid = b.Result;
            Assert.True(isValid);
        }

        [Fact]
        public void registration()
        {
            LogService log = new LogService(new SQLLogDAO());
            var b = log.RegistrationLog("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            bool isValid = b.Result;
            Assert.True(isValid);
        }

        [Fact]
        public void LoginLog()
        {
            LogService log = new LogService(new SQLLogDAO());
            var b = log.LoginLog("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            bool isValid = b.Result;
            Assert.True(isValid);
        }

        [Fact]
        public void registrationChecker()
        {
            SQLLogDAO checker = new SQLLogDAO();
            bool b = checker.RegistrationChecker("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            Assert.True(b);
        }

        [Fact]
        public void loginChecker()
        {
            SQLLogDAO checker = new SQLLogDAO();
            bool b = checker.LoginChecker("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            Assert.True(b);
        }

        [Fact]
        public void getAllTrackerLogs()
        {
            LogService log = new LogService(new SQLLogDAO());
            string logs = log.getAllTrackerLogs();
            Assert.NotEmpty(logs);
        }

        [Fact] 
        public void getAllLogs()
        {
            LogService log = new LogService(new SQLLogDAO());
            String logs = log.getAllLogs();
            Assert.NotEmpty(logs);
        }

        [Fact]
        public void insertRegistration()
        {
            SQLLogDAO ins = new SQLLogDAO();
            bool isValid = ins.RegistrationInsert("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            Assert.False(isValid);
        }

        [Fact]
        public void insertLogin()
        {
            SQLLogDAO ins = new SQLLogDAO();
            bool isValid = ins.LoginInsert("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            Assert.False(isValid);
        }

        [Fact]
        public void updateRegistration()
        {
            SQLLogDAO up = new SQLLogDAO();
            bool isValid = up.RegistrationLog("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            Assert.True(isValid);
        }

        [Fact]
        public void updateLogin()
        {
            SQLLogDAO up = new SQLLogDAO();
            bool isValid = up.LoginLog("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            Assert.True(isValid);
        }


        /*
        [Theory]
        [InlineData("bossAdmin12", "SAS", "business", "error")]
        public void registration(string username, string description, string LogLevel, string LogCategory)
        {
            LogService log = new LogService(new SQLLogDAO());
            var regi = log.RegistrationLog(username, description, LogLevel, LogCategory);
            bool isValid = regi.Result;
            Assert.True(isValid);
            

        }

        
        [Fact]
        public void registration()
        {
            LogService log = new LogService(new SQLLogDAO());
            var b = log.RegistrationLog("bossAdmin12", "Registration- Email Verified", "Data Store", "Verified");
            bool isValid = b.Result;
            Assert.True(isValid);
        }
        */
    }
}