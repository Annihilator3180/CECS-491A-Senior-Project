using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class RecoveryResetManager
    {
        private RecoveryResetService _rS;
        public string connection;

        public bool RecoveryReset()
        {
            DateTime dT = DateTime.Now;
            DateTime baseTime = new DateTime(dT.Year, dT.Month, dT.Day, 1, 0, 0, 0);
            DateTime firstDayOfMonth = baseTime.AddMonths(1);
            _rS = new RecoveryResetService(new RecoveryResetDAO(connection));
            new Task (() => _rS.RecoveryResetScheduler(firstDayOfMonth)).Start();
            return true;
        }
    }
    
}
