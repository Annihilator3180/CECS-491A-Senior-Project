using Microsoft.VisualStudio.TestTools.UnitTesting;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealh.DAL.Tests
{
    [TestClass]
    public class CreateAccountUMDaoShould
    {

        // TODO : GET RAMI FROM DB THEN DELETE FOR SUCCESS
        [TestMethod]
        public void InsertTest()
        {


            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            User ret = UmDAO.Read(rami);
            UmDAO.Delete(rami);
            Assert.AreEqual(rami.Username, ret.Username );
        }

        [TestMethod]
        public void UpdateTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
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
            System.Diagnostics.Debug.WriteLine(updated.Email);
            UmDAO.Delete(rami);
            Assert.AreNotEqual(original.Email, updated.Email);
        }

        [TestMethod]
        public void ReadTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.FirstName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            User original = UmDAO.Read(rami);
            UmDAO.Delete(rami);

            Assert.AreEqual(rami.Email,original.Email);
            Assert.AreEqual(rami.LastName, original.LastName);
            Assert.AreEqual(rami.IsAdmin, original.IsAdmin);
            Assert.AreEqual(rami.Password, original.Password);
            Assert.AreEqual(rami.IsEnabled, original.IsEnabled);
            Assert.AreEqual(rami.FirstName, original.FirstName);
            Assert.AreEqual(rami.Username, original.Username);



        }

        [TestMethod]
        public void EnableTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 0;
            UmDAO.Create(rami);
            UmDAO.EnableAccount("testname");
            rami.IsEnabled = 1;
            User updated = UmDAO.Read(rami);
            System.Diagnostics.Debug.WriteLine(updated.IsEnabled);

            UmDAO.Delete(rami);
            Assert.AreEqual(1, updated.IsEnabled);
        }


        [TestMethod]
        public void DisableTest()
        {
            SqlUMDAO<User> UmDAO = new SqlUMDAO<User>();
            User rami = new User();
            rami.Username = "testname";
            rami.LastName = "test";
            rami.Email = "test@gmail.com";
            rami.IsAdmin = 0;
            rami.Password = "testpass123";
            rami.IsEnabled = 1;
            UmDAO.Create(rami);
            rami.IsEnabled = 0;

            UmDAO.DisableAccount("testname");
            User updated = UmDAO.Read(rami);
            UmDAO.Delete(rami);
            System.Diagnostics.Debug.WriteLine(updated.IsEnabled);
            Assert.AreEqual(0, updated.IsEnabled);
        }

    }
}