using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Authorization.Contract;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class AuthManager
    {

        private AuthenticationService _AS;
        public IAuthorizationService Authorization;

        public AuthManager(IRepositoryAuth<string> daotype)
        {
            _AS = new AuthenticationService(daotype);

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

            return Authorization.generateToken($"{username};Admin");

        }
    }
}
