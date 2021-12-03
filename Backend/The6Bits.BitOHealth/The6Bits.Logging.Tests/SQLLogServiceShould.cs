using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using The6Bits.Logging.Implementations;
using The6Bits.Logging;

namespace The6Bits.Logging.Tests
{
    [TestClass]

    public class SQLLogServiceShould
    {
        
        //TODO:FIX TEST
        [TestMethod]
        public void GetLog()
        {
            var logService = new SQLLogService();

            logService.Log("agazaaiiin", "Ne Dis", "Inftion", "ksajldsa");
            string all = logService.getAllLogs();
            Assert.IsTrue(all.Contains("agazaaiiin Ne Dis Inftion ksajldsa"));

        }

    }
}
