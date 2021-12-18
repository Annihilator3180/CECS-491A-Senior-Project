using Xunit;

namespace The6Bits.Logging.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var logService = new LogService(new SQLLogDAO());
            string testusername = "some";
            _ = logService.Log(testusername, "TEST", "Info", "Data");
            string all = logService.getAllLogs();
            Debug.Assert(all.Contains(testusername));
        }
    }
}