using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class AccountService
    {
        private IRepositoryAuth<string> _AD;
        public AccountService(IRepositoryAuth<string> daotype)
        {
            _AD = daotype;

        }

        public string UsernameExists(string username) 
        {
            return _AD.UsernameExists(username);
        }

        public string UserRole(string username)
        {
            return _AD.UserRole(username);
        }

        public string CheckPassword(string username, string password)
        {
            return _AD.CheckPassword(username, password);
        }

        public string GetEmail(string username)
        {
            return _AD.Read(new User(){Username = username}).Email;
        }

        public string DeletePastOTP(string username, string codeType)
        {
            return _AD.DeletePastOTP(username, codeType);
        }

        public string SaveActivationCode(string username, DateTime time, string code, string codeType)
        {
            return _AD.SaveActivationCode(username,time,code,codeType);
        }

        public string IsEnabled(string username)
        {
            try
            {
                if (_AD.Read(new User() {Username = username}).IsEnabled == 1)
                {
                    return "enabled";
                }
                else
                {
                    return "disabled";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

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

        public bool ValidatePassword(string password)
        {
            try
            {
                if ((password.Length >= 8 & password.Length <= 30) & (password.Contains('.') || password.Contains(',') || password.Contains('!') || password.Contains('@')))
                {
                    password = password.Replace("@", string.Empty).Replace(",", String.Empty).Replace("!", String.Empty).Replace(".", String.Empty);
                }
                else
                {
                    return false;
                }
                return password.All(char.IsLetterOrDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
            }
            catch
            {
                return false;
            }
        }


        public string ValidateUsername(string username)
        {
            List<char> charsToRemove = new List<char>() { '@', '!', ',', '.' };
            string usernametest = username.Replace("@", string.Empty).Replace(",", String.Empty).Replace("!", String.Empty).Replace(".", String.Empty);
            if (!usernametest.All(Char.IsLetterOrDigit) || username.Length > 16 || username.Length <= 6)
            {
                return "Invalid Username";
            }
            if (_AD.UsernameExists(username)== "username exists")
            {
                return "username exists";
            }

            return "new username";
        }


        public string ValidateOTP(string username, string code)
        {
            string res = _AD.ValidateOTP(username, code);
            if (res == "1")
            {
                return "valid";
            }
            if (res == "0")
            {
                return "invalid OTP";
            }

            return res;
        }

        public string CheckFailedAttempts(string username)
        {
            return _AD.CheckFailedAttempts(username);
        }

        public string CheckFailDate(string username)
        {
            return _AD.CheckFailDate(username);
        }

        public string InsertFailedAttempts(string username)
        {
            return _AD.InsertFailedAttempts(username);
        }

        public string UpdateFailedAttempts(string username, int updatedValue)
        {
            string lineschanged = _AD.UpdateFailedAttempts(username, updatedValue);

            if (lineschanged == "1")
            {
                return "updated attempts";
            }
            else
            {
                return lineschanged;
            }
        }

        public string SaveUnActivatedAccount(User user)
        {
            String unactivated = _AD.UnactivatedSave(user);
        }

        public string UpdateIsEnabled(string username, int updateValue)
        {
            string res = _AD.UpdateIsEnabled(username, updateValue);
            if (res == "1")
            {
                return "account updated";
            }

            return res;
        }

        public string DeleteFailedAttempts(string username)
        {
            return _AD.DeleteFailedAttempts(username);
        }


    }
}
