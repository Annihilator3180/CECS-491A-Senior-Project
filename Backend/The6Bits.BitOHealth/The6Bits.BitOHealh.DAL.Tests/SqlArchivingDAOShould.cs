using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class SqlArchivingDaoShould
    {
        [System.Diagnostics.Conditional("DEBUG")]

        public void TestMethod1()
        {

            SqlArchivingDAO dao = new SqlArchivingDAO();
            IList<string> s = dao.GetLogsOlderThan30Days(DateTime.Now);
            dao.Delete(DateTime.Now);
            foreach (var v in s)
            {
                System.Diagnostics.Debug.WriteLine(v);

            }
            Debug.Assert(1==1);


        }

    }
}