using Xunit;
using System.Diagnostics;
using The6Bits.BitOHealth.Logging;
using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Implementations;

namespace The6Bits.BitOHealth.Logging.Test
{
    public class UnitTest1
    {
        [Fact]
        public void CreateTest()
        {
            var logService = new LogService(new SQLLogDAO());
            string testusername = "Some";
            _ = logService.Log(testusername, "TEST", "Info", "Data");
            string all = logService.getAllLogs();
            Debug.Assert(all.Contains(testusername));

        }
    }
}