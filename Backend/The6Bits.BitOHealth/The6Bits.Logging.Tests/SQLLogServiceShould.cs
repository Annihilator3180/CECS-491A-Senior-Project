using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Dapper;
using The6Bits.Logging;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.Logging.Implementations;

namespace The6Bits.Logging.Tests
{

    public class SQLLogServiceShould
    {

        //TODO:ADD MORE TESTS
        [System.Diagnostics.Conditional("DEBUG")]

        public void InsertTest()
        {
            var logService = new LogService(new SQLLogDAO());
            string testusername = RandomString(6);
            _ = logService.Log(testusername, "TEST", "Info", "Data");
            string all = logService.getAllLogs();
            Debug.Assert(all.Contains(testusername));

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
