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

            logService.Log("user22", "New Acc Dis", LogLevel.Information, LogCategory.Datastore);

        }
    }
}
