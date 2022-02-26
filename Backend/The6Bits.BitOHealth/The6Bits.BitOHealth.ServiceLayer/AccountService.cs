using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;


using The6Bits.EmailService;
namespace The6Bits.BitOHealth.ServiceLayer
{
    public class AccountService
    {
        private IRepositoryAuth<string> _AD;
        private IDBErrors _DBErrors;
        private ISMTPEmailServiceShould _EmailService;
        public AccountService(IRepositoryAuth<string> daotype,IDBErrors DbError, ISMTPEmailServiceShould EmailService)
        {
             _DBErrors= DbError;
            _AD = daotype;
            _EmailService= EmailService;



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
            String daoResult = _AD.UsernameExists(username);
            if (!usernametest.All(Char.IsLetterOrDigit) || username.Length > 16 || username.Length <= 6)
            {
                return "Invalid Username";
            }
            if (daoResult == "username exists")
            {
                return "username exists";
            }
            else if(daoResult !="username not found")
            {
                return _DBErrors.DBErrorCheck(int.Parse(daoResult));
            }

            return "new username";
        }

        public string VerifySameDay(string code, string username, DateTime now)
        {
            String CreationTime=_AD.GetTime(code,username);
            try
            {
                DateTime ExpirationDate = DateTime.Parse(CreationTime);
                ExpirationDate.AddDays(1);
                if (now > ExpirationDate)
                {
                    return "True";
                }
                return "Code Expired";
                    }
            catch(Exception)
            {
                return _DBErrors.DBErrorCheck(int.Parse(CreationTime));
            }
            
        }

        public async Task<String> DeleteCode(string username,string codeType)
        {
            return _AD.DeleteCode(username,codeType);
        }

        public string VerifyAccount(string username)
        {

           string codeinDB = _AD.getCode(username, "Registration");
            if (codeinDB.Length < 10)
            {
                return _DBErrors.DBErrorCheck(int.Parse(codeinDB));
            }
            return codeinDB;
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

        public async Task<string> EmailFailed(User user)
        {
            String DeletionStatus=_AD.DeleteUnActivated(user);
            if (DeletionStatus != "1")
            {
                return _DBErrors.DBErrorCheck(int.Parse(DeletionStatus));
            }
            return "True";
        }

        //TODO: Finish implementing email
        public string VerifyEmail(string username, string email, DateTime now)
        {
            String code=Guid.NewGuid().ToString("N");
            String saveStatus=_AD.SaveActivationCode(username, now, code, "Registration");
            if (saveStatus != "Saved")
            {
                return _DBErrors.DBErrorCheck(int.Parse(saveStatus));
            }

            String Subject = "Verify your account";
            String Body = "Please use this link to verify your account https://localhost:7011/Account/VerifyAccount?Code=" + code + "&&Username=" + username;
            String EmailStatus = _EmailService.SendEmail(email,Subject,Body);
            if (EmailStatus != "email sent")
            {
                return EmailStatus;
            }
            return "True";
        }

        public string SaveUnActivatedAccount(User user)
        {
            String unactivated = _AD.UnactivatedSave(user);
            if (unactivated != "Saved")
            {
                return _DBErrors.DBErrorCheck(int.Parse(unactivated));
            }
            return "Saved";
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

        public string AcceptEULA(string username)
        {
            /* add DB error 
            string codeinDB = _AD.getCode(username, "UserPrivacy");
            if (codeinDB.Length > 10)
            {
                return _DBErrors.DBErrorCheck(int.Parse(codeinDB));
            }
            return codeinDB;
            */
            string res = _AD.AcceptEULA(username);
            if (res == "1")
            {
                return "Privacy Accepted";
            }
            return res;
        }

        public string DeclineEULA(string username)
        {
            /* add DB error 
            string codeinDB = _AD.getCode(username, "UserPrivacy");
            if (codeinDB.Length > 10)
            {
                return _DBErrors.DBErrorCheck(int.Parse(codeinDB));
            }
            return codeinDB;
            */
            string res = _AD.AcceptEULA(username);
            if (res == "1")
            {
                return "Privacy Declined";
            }
            return res;
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
    }




}

