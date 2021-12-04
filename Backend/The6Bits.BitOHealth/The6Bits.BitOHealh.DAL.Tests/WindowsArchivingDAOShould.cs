using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Tests
{
    [TestClass]
    public class WindowsArchivingDaoShould
    {
        [TestMethod]
        public void ArchiveTest()
        {
            WindowsArchivingDAO dao = new WindowsArchivingDAO();
            SqlArchivingDAO sqldao = new SqlArchivingDAO();
            dao.Archive(sqldao.GetLogsOlderThan30Days(DateTime.Now));
            System.Environment.GetEnvironmentVariable("USERPROFILE");

            Assert.AreEqual(1, 1);


        }

        [TestMethod]
        public void CompressTest()
        {
            WindowsArchivingDAO dao = new WindowsArchivingDAO();
            SqlArchivingDAO sqldao = new SqlArchivingDAO();
            dao.Archive(sqldao.GetLogsOlderThan30Days(DateTime.Now));
            dao.Compress();
            Assert.AreEqual(1, 1);

        }


    }
}