using System;
using System.ComponentModel.Design;
using The6Bits.Authorization;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging;
using The6Bits.Logging.Implementations;
using The6Bits.Authorization.Contract;
using The6Bits.HashAndSaltService;
using The6Bits.HashAndSaltService.Contract;


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class UMManager
    {

        private AuthorizationService _authorizationService;
        public string token;
        private UMService _UMS;
        private HashNSaltService _hashNSaltService;

        public UMManager(IRepositoryUM<User> daoType, IAuthorizationDao _iAuthorizationDao, IHashDao hashDao)
        {
            _authorizationService = new AuthorizationService(_iAuthorizationDao);
            _hashNSaltService = new HashNSaltService(hashDao);
            _UMS = new UMService(daoType);
        }

        public string CreateAccount(User user)
        {
            
            string validation = _UMS.ValidateUsername(user.Username);
            bool emailval = _UMS.ValidateEmail(user.Email);
            bool passval = _UMS.ValidatePassword(user.Password);
            if (validation != "new username")
            {
                return validation;
            }
            if (!emailval)
            {
                return "invalid email";
            }
            if (!passval)
            {
                return "invalid password";
            }

            return _UMS.CreateAccount(user);
        }

        public string DeleteAccount(string username)
        {
            string validation = _UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            string response = _UMS.DeleteAccount(username);
            return response;
        }

        public string EnableAccount(string username)
        {
            string validation = _UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            return _UMS.EnableAccount(username);
        }

        public string UpdateAccount(User user)
        {

            string validation = _UMS.ValidateUsername(user.Username);
            if (validation != "username exists")
            {
                return validation;
            }
            if (user.Email != null)
            {
                if (!_UMS.ValidateEmail(user.Email))
                {
                    return "invalid email";
                }
            }

            if (user.Password != null) 
            {
                if (!_UMS.ValidatePassword(user.Password))
                {
                    return "invalid password";
                }
            }


            string response = _UMS.UpdateAccount(user);
            
            return response;


        }

        public string DisableAccount(string username)
        {

            _UMS = new UMService(new MsSqlUMDAO<User>());
            string validation = _UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            string response = _UMS.DisableAccount(username);
            
            return response;
        }

        

    }
}