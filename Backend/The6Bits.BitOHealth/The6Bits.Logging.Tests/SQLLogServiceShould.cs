using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using The6Bits.Logging.Implementations;
using The6Bits.Logging;

namespace The6Bits.Logging.Tests
{
    [TestClass]

    public class SQLLogServiceShould
    {

        //TODO:ADD MORE TESTS
        [TestMethod]
        public void InsertTest()
        {
            var logService = new SQLLogService(new SQLLogDAO());
            string testusername = RandomString(6);
            _ = logService.Log(testusername, "TEST", "Info", "Data");
            string all = logService.getAllLogs();
            Assert.IsTrue(all.Contains(testusername));

        }

        [TestMethod]
        public void ReadTest()
        {

        }
        private string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            foreach (var i in Enumerable.Range(0, size))
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

    }
}
