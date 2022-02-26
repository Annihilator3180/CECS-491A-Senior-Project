
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests
{

    public class UMDaoShould
    {

        [Fact]
        public void CreateTest()
        {
                MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
                User Test = new User();
                Test.Username = "createname";
                Test.LastName = "test";
                Test.FirstName = "test2";
                Test.Email = "test@gmail.com";
                Test.IsAdmin = 0;
                Test.Password = "testPass123!";
                Test.IsEnabled = 1;
            Test.privOption = 1;
                Stopwatch stopwatch = Stopwatch.StartNew();
                bool valid = UmDAO.Create(Test);
                stopwatch.Stop();
                double stopwatchTime = stopwatch.ElapsedMilliseconds;
                UmDAO.Delete(Test);
                Assert.True(valid);
                Assert.True(3000 > stopwatchTime);
    }

    [Fact]
    public void testDelete()
    {
        MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
        User Test = new User();
        Test.Username = "deletetest";
        Test.LastName = "test";
        Test.FirstName = "test2";
        Test.Email = "test@gmail.com";
        Test.IsAdmin = 0;
        Test.Password = "testpass123";
        Test.IsEnabled = 1;
        Test.privOption = 1;
        Test.Email = "test2@gmail.com";
        UmDAO.Create(Test);
        Stopwatch stopwatch = Stopwatch.StartNew();
        bool valid = UmDAO.Delete(Test);
        stopwatch.Stop();
        double stopwatchTime = stopwatch.ElapsedMilliseconds;
        Assert.True(valid);
        Assert.True(3000 > stopwatchTime);
        }

    [Fact]
    public void UpdateDaoTest()
    {

        MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
        User Test = new User();
        Test.Username = "updatest";
        Test.LastName = "test";
        Test.Email = "test@gmail.com";
        Test.IsAdmin = 0;
        Test.Password = "testPass123!";
        Test.IsEnabled = 1;
        Test.privOption = 1;
        UmDAO.Create(Test);
        Test.Email = "test2@gmail.com";
        Stopwatch stopwatch = Stopwatch.StartNew();
        bool valid = UmDAO.Update(Test);
        stopwatch.Stop();
        UmDAO.Delete(Test);
        double stopwatchTime = stopwatch.ElapsedMilliseconds;
        Assert.True(valid);
        Assert.True(3000 > stopwatchTime);

    }




    [Fact]
    public void ReadTest()
    {
        MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
        User Test=new User();
        Test.Username = "readtest";
        Test.LastName = "test";
        Test.FirstName = "test";
        Test.Email = "test@gmail.com";
        Test.IsAdmin = 0;
        Test.Password = "testpass123";
        Test.IsEnabled = 1;
        Test.privOption = 1;
        UmDAO.Create(Test);
        Stopwatch stopwatch = Stopwatch.StartNew();
        User original = UmDAO.Read(Test);
        stopwatch.Stop();
        UmDAO.Delete(Test);
        double stopwatchTime = stopwatch.ElapsedMilliseconds;
        Assert.Equal("readtest", original.Username);
        Assert.True(3000 > stopwatchTime);

    }



    [Fact]
    public void ReadAfterDeleteTest()
    {
        MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
        User Test = new User();
        Test.Username = "rafterdtest";
        Test.LastName = "test";
        Test.FirstName = "test";
        Test.Email = "test@gmail.com";
        Test.IsAdmin = 0;
        Test.Password = "testpass123";
        Test.IsEnabled = 1;
        Test.privOption = 1;
        UmDAO.Create(Test);
        UmDAO.Delete(Test);
        Stopwatch stopwatch = Stopwatch.StartNew();
        User original = UmDAO.Read(Test);
        stopwatch.Stop();
        double stopwatchTime = stopwatch.ElapsedMilliseconds;
        Assert.Null(original.Email);
        Assert.True(3000 > stopwatchTime);

    }





    [Fact]
    public void EnableTest()
    {
        MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
        User Test = new User();
        Test.Username = "enabletest";
        Test.LastName = "test";
        Test.Email = "test@gmail.com";
        Test.IsAdmin = 0;
        Test.Password = "testpass123";
        Test.IsEnabled = 0;
        Test.privOption = 1;
        UmDAO.Create(Test);
        Stopwatch stopwatch = Stopwatch.StartNew();
        bool valid =UmDAO.EnableAccount("enabletest");
        UmDAO.Delete(Test);
        stopwatch.Stop();
        double stopwatchTime = stopwatch.ElapsedMilliseconds;
        Assert.True(valid);
        Assert.True(3000 > stopwatchTime);

    }


    [Fact]
    public void DisableTest()
    {
        MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
        User Test = new User();
        Test.Username = "disabletest";
        Test.LastName = "test";
        Test.Email = "test@gmail.com";
        Test.IsAdmin = 0;
        Test.Password = "testpass123";
        Test.IsEnabled = 0;
        Test.privOption = 1;
        UmDAO.Create(Test);
        Stopwatch stopwatch = Stopwatch.StartNew();
        bool valid = UmDAO.EnableAccount("disabletest");
        UmDAO.Delete(Test);
        stopwatch.Stop();
        double stopwatchTime = stopwatch.ElapsedMilliseconds;
        Assert.True(valid);
        Assert.True(3000 > stopwatchTime);
        }




        [Fact]
        public void ValidZero()
        {
            MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
            bool isValid = UmDAO.UsernameExists("countcheck");
            Assert.False(isValid);
        }
        [Fact]
        public void TestDuplicateExists()
        {
            MsSqlUMDAO<User> UmDAO = new MsSqlUMDAO<User>();
            User Test = new User();
            Test.Username = "duplicate";
            Test.LastName = "test";
            Test.Email = "test@gmail.com";
            Test.IsAdmin = 0;
            Test.Password = "testpass123";
            Test.IsEnabled = 0;
            Test.privOption = 1;
            UmDAO.Create(Test);
            bool isValid = UmDAO.UsernameExists("duplicate");
            UmDAO.Delete(Test);
            Assert.True(isValid);
        }
    }
}