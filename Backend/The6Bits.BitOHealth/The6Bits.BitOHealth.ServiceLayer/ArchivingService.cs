using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.Logging;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Logging.Implementations;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.Logging.DAL.Implementations;


namespace The6Bits.BitOHealth.ServiceLayer
{
    public class ArchivingService
    {
        private readonly IArchivingPC _pc;
        private readonly IArchivingDatabase _db;
        private readonly LogService _logService;

        public ArchivingService(IArchivingPC archivetype, IArchivingDatabase logger)
        {
            _pc = archivetype;
            _db = logger;
            _logService = new LogService(new SQLLogDAO());
        }

        public async void ArchiveScheduler(DateTime executionTime)
        {
            while (true)
            {
                await Task.Delay((int)executionTime.Subtract(DateTime.Now).TotalMilliseconds);
                Archive();
                executionTime.AddMonths(1);
            }
        }



        public void Archive()
        {
            DateTime dtNow = DateTime.Now;


            if (!_pc.Archive(_db.GetLogsOlderThan30Days(dtNow)))
            {
                _logService.Log("admin", "error deleting logs", "Error", "Date Store");
                return;

            }
            if (!_pc.Compress())
            {
                _logService.Log("admin", "error compressing logs", "Error", "Date Store");
                return;
            }
            if (!_db.Delete(dtNow))
            {
                _logService.Log("admin", "error deleting csv", "Error", "Date Store");
                return;
            }


        }


    }
}
