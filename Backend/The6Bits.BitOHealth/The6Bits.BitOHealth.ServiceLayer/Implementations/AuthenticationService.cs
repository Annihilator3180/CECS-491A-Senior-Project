using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.ServiceLayer.Implementations
{
    public class AuthenticationService
    {
        private IRepositoryAuth<string> _AD;
        public AuthenticationService(IRepositoryAuth<string> daotype)
        {
            _AD = daotype;

        }
    }
}
