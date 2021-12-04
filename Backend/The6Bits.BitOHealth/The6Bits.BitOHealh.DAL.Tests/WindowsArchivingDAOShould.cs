using System;
using System.Collections.Generic;
using System.Diagnostics;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class WindowsArchivingDaoShould
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public void ArchiveTest()
        {
            WindowsArchivingDAO dao = new WindowsArchivingDAO();
            SqlArchivingDAO sqldao = new SqlArchivingDAO();
            dao.Archive(sqldao.GetLogsOlderThan30Days(DateTime.Now));
            System.Environment.GetEnvironmentVariable("USERPROFILE");

            Debug.Assert(1== 1);


        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void CompressTest()
        {
            WindowsArchivingDAO dao = new WindowsArchivingDAO();
            SqlArchivingDAO sqldao = new SqlArchivingDAO();
            dao.Archive(sqldao.GetLogsOlderThan30Days(DateTime.Now));
            dao.Compress();
            Debug.Assert(1== 1);

        }


    }
}