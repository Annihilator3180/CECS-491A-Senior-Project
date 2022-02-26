using Xunit;
using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Implementations;

namespace The6Bits.Logging.Test
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("bossAdmin12", "SAS", "business", "error")]
        public void Test1(string username, string description, string LogLevel, string LogCategory)
        {
            LogService log = new LogService(new SQLLogDAO());
            log.RegistrationLog(username, description, LogLevel, LogCategory);

        }
    }
}