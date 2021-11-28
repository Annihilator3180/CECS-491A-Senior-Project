using Microsoft.VisualStudio.TestTools.UnitTesting;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealh.DAL.Tests
{
    [TestClass]
    public class CreateAccountDALShould
    {

        // TODO : GET RAMI FROM DB THEN DELETE FOR SUCCESS
        [TestMethod]
        public void TestMethod1()
        {

            UserManagementDAL<User> userManagementDAL = new UserManagementDAL<User>();
            User rami = new User();
            //rami.FirstName = "Rami";
            //rami.LastName = "Isder";
            rami.Username = "boofman2";
            //rami.Email = "b@gmail.com";
            //rami.IsAdmin = 1;
            //rami.Password = "Boof";
            //rami.IsEnabled = 1;
            userManagementDAL.Read(rami);
            

        }
    }
}