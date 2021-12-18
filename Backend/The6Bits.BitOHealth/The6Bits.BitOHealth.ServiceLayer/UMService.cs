using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class UMService 
    {
        private IRepositoryUM<User> _dao;

        public UMService(IRepositoryUM<User> daoType)
        {
            _dao = daoType;
        }

        public bool ValidateEmail(string email)
        {
            try
            {
                
                return new EmailAddressAttribute().IsValid(email) && email.Length < 255;
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
                if (password.Length >= 8 & password.Length <= 30 & (password.Contains('.') || password.Contains(',') || password.Contains('!') || password.Contains('@')))
                {
                    password = password.Remove('.').Remove(',').Remove('!').Remove('@');
                }
                else
                {
                    return false;
                }
                return password.Any(char.IsLetterOrDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
            }
            catch
            {
                return false;
            }
        }

       
        public string ValidateUsername(string username)
        {
            User user = new User()
            {
                Username = username
            };
            username = username.Remove('.').Remove('@').Remove('!') ;
            if (!username.Any(Char.IsLetterOrDigit) & username.Length < 16 & username.Length > 6)
            {
                return "Invalid Username";
            }
            if (_dao.UsernameExists(username))
            {
                return "username exists";
            }

            return "new username";
        }


        public string CreateAccount(User user)
        {
            return _dao.Create(user) ? "account created" : "database error";
        }

        public string DeleteAccount(string username)
        {
            User user = new User()
            {
                Username = username
            };
            return _dao.Delete(user) ? "account Deleted" : "database error";
        }

        public string UpdateAccount(User user)
        {
            return _dao.Update(user) ? "account update" : "database error";
        }


        public string EnableAccount(string username)
        {
            return _dao.EnableAccount(username) ? "account enabled" : "database error";
        }

        public string DisableAccount(string username)
        {
            return _dao.DisableAccount(username) ? "account disabled" : "database error";
        }

       

       



    }
}