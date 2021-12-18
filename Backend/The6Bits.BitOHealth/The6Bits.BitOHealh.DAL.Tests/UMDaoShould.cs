using System;
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
        public void InsertTest()
        {

            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "inserttest";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Delete(rami);
            bool s = UmDAO.Create(rami);
            UmDAO.Delete(rami);
            Assert.True(s);

        }



        [Fact]
        public void UpdateTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "updatest";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;

            UmDAO.Create(rami);
            User original = UmDAO.Read(rami);
            rami.Email = "test2@gmail.com";
            UmDAO.Update(rami);
            User updated = UmDAO.Read(rami);
            UmDAO.Delete(rami);
            Assert.Equal("test2@gmail.com" , updated.Email);

        }



        [Fact]
        public void ReadTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "readtest";
            rami.LastName = "test";
            rami.FirstName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;

            UmDAO.Delete(rami);
            UmDAO.Create(rami);
            User original = UmDAO.Read(rami);
            UmDAO.Delete(rami);

            Assert.Equal(rami.Email,original.Email);
           
        }

        [Fact]
        public void ReadAfterDeleteTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "rafterdtest";
            rami.LastName = "test";
            rami.FirstName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            UmDAO.Delete(rami);
            User original = UmDAO.Read(rami);
            Assert.Null(original.Email);
        }





        [Fact]
        public void EnableTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "enabletest";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 0;
            UmDAO.Create(rami);
            UmDAO.EnableAccount("enabletest");
            User updated = UmDAO.Read(rami);
            UmDAO.Delete(rami);
            Assert.Equal(1, updated.IsEnabled);
        }

        [Fact]
        public void DisableTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "disabletest";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 1;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            UmDAO.DisableAccount("disabletest");
            User updated = UmDAO.Read(rami);
            UmDAO.Delete(rami);
           
            Assert.Equal(0 , updated.IsEnabled);
        }



        [Fact]
        public void DeleteTest()
        {

            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "deletetest";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            UmDAO.Delete(rami);
            User ret = UmDAO.Read(rami);
            Assert.Null(ret.Email);
        }

    }
}