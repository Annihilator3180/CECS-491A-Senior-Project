using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class AuthManager
    {

        private AuthenticationService _AS;

        public AuthManager(IRepositoryAuth<string> daotype)
        {
            _AS = new AuthenticationService(daotype);

        }
    }
}
