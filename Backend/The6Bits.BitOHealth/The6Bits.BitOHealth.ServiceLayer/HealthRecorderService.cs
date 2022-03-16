using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class HealthRecorderService
    {
        private IRepositoryHealthRecorderDAO _HRD;

        public HealthRecorderService(IRepositoryHealthRecorderDAO dao)
        {
            _HRD = dao;
        }
    }
}
