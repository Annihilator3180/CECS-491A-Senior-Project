using System;
using System.Collections.Generic;
using System.IO.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Tests
{
    [TestClass]
    public class SqlArchivingDaoShould
    {
        [TestMethod]
        public void TestMethod1()
        {

            SqlArchivingDAO dao = new SqlArchivingDAO();
            IList<string> s = dao.GetLogsOlderThan30Days(DateTime.Now);
            //dao.Delete(DateTime.Now);
            foreach (var v in s)
            {
                System.Diagnostics.Debug.WriteLine(v);

            }
            Assert.AreEqual(1,1);


        }

    }
}