using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class ArchivingManager
    {
        private ArchivingService _archivingService;
        public string conn;

        public bool Archive()
        {
            DateTime Now = DateTime.Now;
            Now = Now.AddMonths(1);
            DateTime date = new DateTime(Now.Year, Now.Month, 1, 0, 0, 0);
            Console.WriteLine(date);

            _archivingService = new ArchivingService(new WindowsArchivingDAO(), new SqlArchivingDAO() { _connectString = conn});
            _archivingService.ArchiveScheduler(date);
            return true;


        }
    }
}
