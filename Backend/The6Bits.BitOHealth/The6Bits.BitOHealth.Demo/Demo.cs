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
using The6Bits.Logging;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.Demo
{
    class Demo
    {
        public static void Main(string[] args)
        {
            CreateAccountUMDaoShould i = new CreateAccountUMDaoShould();
            i.InsertTest();
            ArchivingController _archivingController;
            _archivingController = new ArchivingController(new ArchivingService(new WindowsArchivingDAO(), new SqlArchivingDAO()));
            
            
            
            UMController um=new UMController();
            User rami = new User("Rami", "ramiz@gmail.com", "123Rr567","Rami", "Iskender",1,1);
            String RamiCreate = um.CreateAccount(rami);
            Console.WriteLine(RamiCreate);
            String RamiDisable = um.DisableAccount("Rami");
            Console.WriteLine(RamiDisable);
            String RamiEnable = um.EnableAccount("Rami");
            Console.WriteLine(RamiEnable);
            User rami2= new User("Rami", "rami@gmail.com", "123Rr567", "Ramy", "Saleh", 1, 1);
            String RamiCreate2 = um.CreateAccount(rami2);
            Console.WriteLine(RamiCreate2);
            String RamiDelete=um.DeleteAccount("Rami");
            Console.WriteLine(RamiDelete);
            IList<string> rands = um.BulkCreateRandom(10);
            um.BulkDelete(rands);




        }




    }
}
