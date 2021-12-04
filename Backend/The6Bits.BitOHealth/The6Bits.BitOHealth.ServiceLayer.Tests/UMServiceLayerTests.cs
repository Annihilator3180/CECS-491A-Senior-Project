using System.Diagnostics;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.ServiceLayer.Contract;
using The6Bits.BitOHealth.ServiceLayer.Implementations;

namespace The6Bits.BitOHealth.ServiceLayer.Tests
{
    public class UMServiceLayerTests
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public void UpdateServiceTest()
        {
            IUMService umService = new UMService(new SqlUMDAO<User>());
            User test = new User();
            test.Username = "testname";
            test.Password = "passA12112";

            umService.CreateAccount(test);
            test.FirstName = "boss";
            umService.UpdateAccount(test);
            Debug.Assert(umService.View("testname").Contains("testname"));


        }
    }
}