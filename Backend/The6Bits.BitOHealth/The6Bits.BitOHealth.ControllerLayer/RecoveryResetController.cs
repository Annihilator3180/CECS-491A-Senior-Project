using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.ManagerLayer;

namespace The6Bits.BitOHealth.ControllerLayer
{
    public class RecoveryResetController
    {
        public string connectionString;
        private RecoveryResetManager _rM;

        public bool ResetRecovery(string conn)
        {
            connectionString = conn;
            _rM = new RecoveryResetManager() { connection = connectionString};
            return _rM.RecoveryReset();
        }
    }
}
