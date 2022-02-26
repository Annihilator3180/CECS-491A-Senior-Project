using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            string email =  _AD.Read(new User(){Username = username}).Email;
            int temp;
            if (!Int32.TryParse(email, out temp))
            {
                return email;
            }
            //call error handler
            return email;
        }

        public string DeletePastOTP(string username, string codeType)
        {
            
            string res = _AD.DeletePastOTP(username, codeType);
            if (res.Contains("deleted"))
            {
                return res;
            }
            else
            {
                //HANDLE ERROR
                return res;
            }
        }

        public string SaveActivationCode(string username, DateTime time, string code, string codeType)
        {
            string res =  _AD.SaveActivationCode(username,time,code,codeType);
            if (res == "1")
            {
                return "saved";
            }
            else
            {
                //handle error
                return "";
            }
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
            
            
            string date =  _AD.CheckFailDate(username);
            if (date == "none")
            {
                return date;
            }

            try
            {
                DateTime.Parse(date);
                return date;
            }
            catch
            {
                return "DB ERROR";
            }


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
            string res = _AD.DeleteFailedAttempts(username);
            if (res == "1")
            {
                return "1";
            }

            return res;
        }


    }
}
