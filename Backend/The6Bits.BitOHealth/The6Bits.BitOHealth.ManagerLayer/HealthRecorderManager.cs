using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class HealthRecorderManager
    {
        private IDBErrors _dbErrors;
        private IRepositoryHealthRecorderDAO _dao;
        private HealthRecorderService _HRS;

        public HealthRecorderManager(IDBErrors dBErrors, IRepositoryHealthRecorderDAO dao)
        {
            _dbErrors = dBErrors;
            _dao = dao;
            _HRS = new HealthRecorderService(dao);
        }
    }
}
