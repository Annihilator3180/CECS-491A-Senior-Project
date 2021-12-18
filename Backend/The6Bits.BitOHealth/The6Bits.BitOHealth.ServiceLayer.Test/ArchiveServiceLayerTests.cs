using System;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;
using Xunit;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Contract;
using System.Diagnostics;

namespace The6Bits.BitOHealth.ServiceLayer.Tests
{
    public class ArchiveServiceLayerTests
    {
        [Fact]
        public void ValidArchiveAge()
        {
            SqlArchivingDAO sArch = new SqlArchivingDAO();
            WindowsArchivingDAO wArch = new WindowsArchivingDAO();
            var timer = new Stopwatch();
            bool expected = true;
            timer.Start();
            bool actual = wArch.Archive(sArch.GetLogsOlderThan30Days(DateTime.Now));
            timer.Stop();
            Assert.True(expected == actual);
            Assert.True(timer.Elapsed.TotalSeconds <= 60);
        }

        [Fact]
        public void ValidArchiveLocation()
        {
            WindowsArchivingDAO wArch = new WindowsArchivingDAO();
            string expected = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\";


        }


    }
}