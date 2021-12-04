using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer.Contract;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class UMService<T> : IUMService<User>
    {
        private IRepository<User> _dao;


        public UMService(IRepository<User> daoType)
        {
            _dao = daoType;
        }

        public bool ValidateEmail(string email)
        {
            try
            {
                return new EmailAddressAttribute().IsValid(email);
            }
            catch
            {
                return false;
            }
        }

        //TODO: DOUBLE CHECK VALIDATIONS
        public bool ValidatePassword(string password)
        {
            try
            {
                return password.Any(char.IsUpper) & password.Any(char.IsLower) & password.Any(char.IsDigit) &
                       password.Length > 8;
            }
            catch
            {
                return false;
            }
        }

        //TODO: DOUBLE CHECK VALIDATIONS
        public string ValidateUsername(string username)
        {
            User user = new User()
            {
                Username = username
            };
            if (!username.Any(char.IsAscii) & username.Length < 15)
            {
                return "Invalid Username";
            }
            else if (_dao.Read(user).Username == username)
            {
                return "Username Exists ";
            }

            return "new username";
        }


        public string CreateAccount(User user)
        {
            return _dao.Create(user);
        }

        //SOLID IF VALIDATEUSERNAME HERE?
        public string DeleteAccount(string username)
        {
            User user = new User()
            {
                Username = username
            };
            return _dao.Delete(user) ? "account Deleted" : "Deletion failed";
        }


        //SOLID IF VALIDATEUSERNAME HERE?

        public string EnableAccount(string username)
        {
            return _dao.EnableAccount(username) ? "account enabled" : "enable failed";
        }

        public string DisableAccount(string username)
        {
            return _dao.DisableAccount(username) ? "account disabled" : "disable failed";
        }
    }
}