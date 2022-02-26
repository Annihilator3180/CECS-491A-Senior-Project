using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.EmailService;
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
        public string UsernameAndEmailExists(string username, string email)
        {
            return _AD.UsernameAndEmailExists(username, email);
        }
        public string IsEnabled(string username)
        {
            return _AD.IsEnabled(username);
        }
        public string ValidateRecoveryAttempts(string username)
        {
            return _AD.ValidateRecoveryAttempts(username);
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
        /*
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
        */

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
        public string SendEmail(string email, string subject, string body)
        {
           SMTPEmailService sMTPEmailService = new SMTPEmailService();
            return sMTPEmailService.SendEmail(email,subject,body);
        }
        public string UpdateRecoveryAttempts(string username)
        {
            return _AD.UpdateRecoveryAttempts(username);
        }

        public string GenerateRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                char c = chars[random.Next(chars.Length)];
                builder.Append(c);

            }
            return builder.ToString();
        }
        public string VerifySameDay(string username, string code)
        {
            string sd = _AD.VerifySameDay(username, code);
            if (sd != "1")
            {
                return "expired link";
            }
            return sd;

        }
        public string ResetPassword(string password, string username)
        {
            return _AD.ResetPassword(password,username);

        }
        public string RemoveRecoveryAttempts(string username)
        {
            return _AD.RemoveRecoveryAttempts(username);
        }

        public string GetRecoveryOTP(string username)
        {
            return _AD.GetRecoveryOTP(username);
        }
        public string GetPassword(string username)
        {
            return _AD.GetPassword(username);
        }
       
    }
   




}

