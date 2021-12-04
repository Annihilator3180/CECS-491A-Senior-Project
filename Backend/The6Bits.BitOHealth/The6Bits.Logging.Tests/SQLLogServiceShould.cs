using System;
using System.Data.SqlClient;
using System.Linq;
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
            string testusername = "testerr2r";
            _ = logService.Log(testusername, "TEST", "Info", "Data");
            string all = logService.getAllLogs();
            Assert.IsTrue(all.Contains(testusername));


        }

        [TestMethod]
        public void ReadTest()
        {

        }

        public string rand(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            char[] arr = new char[8];
            Random random = new Random();
            foreach (int i in Enumerable.Range(1, arr.Length-1))
            {
                arr[i] = chars[random.Next(chars.Length)];
            }
            return new String(arr);

        }
    }
}
