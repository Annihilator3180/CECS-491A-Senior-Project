using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class HealthRecorderMsSqlDAO : IRepositoryHealthRecorderDAO
    {
        private string _connectionString;

        public HealthRecorderMsSqlDAO(string conn)
        {
            _connectionString = conn;
        }
    }
}
