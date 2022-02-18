using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Authorization.Contract;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class AuthManager
    {

        private AccountService _AS;
        public IAuthorizationService Authorization;


        
        public AuthManager(IRepositoryAuth<string> daotype, IAuthorizationService authService)
        {
            _AS = new AccountService(daotype);
            Authorization = authService;

        }

        public string Login(string username, string password)
        {
            string us = _AS.UsernameExists(username);
            if (us != "username exists")
            {
                return us;
            }
            string cp = _AS.CheckPassword(username, password);
            if (cp != "credentials found")
            {
                return cp;
            }

            return Authorization.generateToken($"{username};Admin");

        }

        public string AdminLogin(string username, string password) 
        {
            string us = _AS.UsernameExists(username);
            if (us != "username exists")
            {
                return us;
            }
            string rl = _AS.UserRole(username);
            if (rl  != "Admin")
            {
                return rl;
            
            }
            string cp = _AS.CheckPassword(username, password);
            if (rl != "Admin")
            {
                return cp;
            }

            return Authorization.generateToken($"{username}");

        }
    }
}
