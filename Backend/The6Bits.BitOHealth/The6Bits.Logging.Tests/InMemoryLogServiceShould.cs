
using System;
using The6Bits.Logging.Implementations;
using The6Bits.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace The6Bits.Logging.Tests
{
    [TestClass]
    public class InMemoryLogServiceShould
    {
        [TestMethod]
        public void GetALog()
        {

            var logService = new InMemoryLogService();
            logService.Log("test","test", "Info ", "Data");
            string logs = logService.getAllLogs();
            Assert.IsTrue(logs.Contains("test"));
            Console.WriteLine(logs);

        }
    }
}