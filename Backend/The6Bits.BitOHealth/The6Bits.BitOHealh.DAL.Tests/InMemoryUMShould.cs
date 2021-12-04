using Microsoft.VisualStudio.TestTools.UnitTesting;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealh.DAL.Tests
{
    [TestClass]
    public class InMemoryCreateAccountShould
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            InMemoryUM<User> InMemoryUM = new InMemoryUM<User>();
            User rami = new User();
            rami.FirstName = "Rami";
            rami.LastName = "Isder";
            rami.Email = "b@gmail.com";
            rami.IsAdmin = 1;
            rami.Password = "Boof";
            rami.IsEnabled = 1;
            InMemoryUM.Create(rami);
            

        }
        
    }
}