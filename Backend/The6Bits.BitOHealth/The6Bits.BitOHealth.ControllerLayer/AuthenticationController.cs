using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.Logging;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.Logging.Implementations;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.Authorization.Contract;

namespace The6Bits.BitOHealth.ControllerLayer
{
    public class AuthenticationController
    {
        private LogService _logService = new LogService(new SQLLogDAO());
        private AuthManager _AM;   
        public AuthenticationController (IRepositoryAuth<string> daotype, IAuthorizationService authType)
        {
            _AM = new AuthManager(daotype);
            _AM.Authorization =  authType;
        }

        public string AdminLogin(string username, string password) 
        {
            return _AM.AdminLogin(username,password);
        }
    }
}
