
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

        public AuthenticationController(IRepositoryAuth<string> daotype, IAuthorizationService authType)
        {
            _AM = new AuthManager(daotype,authType);
        }

        public string AdminLogin(string username, string password)
        {
            string res = _AM.AdminLogin(username, password);
            if (res == "DB Error")
            {
                _logService.Log(username, res, "Error", "Data Store");
            }

            _logService.Log(username, res, "Info", "Business");
            return res;
        }
        


        
    }
}
