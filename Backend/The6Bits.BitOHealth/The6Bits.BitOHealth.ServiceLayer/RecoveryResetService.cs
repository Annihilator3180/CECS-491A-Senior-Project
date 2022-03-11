using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class RecoveryResetService
    {
        private readonly IRecoveryResetDatabase _dB;
        private readonly LogService _logService;
        public string _connection;

        public RecoveryResetService(IRecoveryResetDatabase recovery)
        {
            _dB = recovery;
            _logService = new LogService(new SQLLogDAO());

        }
        public async void RecoveryResetScheduler(DateTime firstDayOfMonth)
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                await Task.Delay(firstDayOfMonth - now);
                RecoveryReset();
                firstDayOfMonth.AddMonths(1);

        
               
            }
        }
        public void RecoveryReset()
        {
            DateTime now = DateTime.Now;
            if (!_dB.Delete())
            {
                _logService.Log("admin", "error resetting recovery", "Error", "Date Store");
                return;
            }
        }
    }
}
