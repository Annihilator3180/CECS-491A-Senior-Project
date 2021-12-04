using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.ControllerLayer;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer.Implementations;
using The6Bits.BitOHealth.ServiceLayer.Tests;
using The6Bits.Logging;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.Demo
{
    class Demo
    {
        public static void Main(string[] args)
        {
            CreateAccountUMDaoShould i = new CreateAccountUMDaoShould();
            UMServiceLayerTests tests = new UMServiceLayerTests();
            tests.UpdateServiceTest();
            ArchivingController _archivingController;
            _archivingController = new ArchivingController();
            _archivingController.Archive();
            
            
            
            UMController um=new UMController();
            User rami = new User("Rami", "ramiz@gmail.com", "123Rr567","Rami", "Iskender",1,1);
            String RamiCreate = um.CreateAccount(rami);
            Console.WriteLine("Account Created ! : " + RamiCreate);
            String RamiDisable = um.DisableAccount("Rami");
            Console.WriteLine("Account Disabled ! : " + RamiDisable);
            String RamiEnable = um.EnableAccount("Rami");
            Console.WriteLine("Account Enabled ! : " + RamiEnable);
            User rami2= new User("Rami", "rami@gmail.com", "123Rr567", "Ramy", "Saleh", 1, 1);
            String RamiCreate2 = um.CreateAccount(rami2);
            Console.WriteLine("Account Creation Failed ! : " + RamiCreate2);
            String RamiDelete=um.DeleteAccount("Rami");
            Console.WriteLine("Account Deleted ! : " + RamiDelete);
            IList<string> rands = um.BulkCreateRandom(10000);
            um.BulkDelete(rands);
            Console.WriteLine("10000 accs made and deleted");



        }




    }
}
