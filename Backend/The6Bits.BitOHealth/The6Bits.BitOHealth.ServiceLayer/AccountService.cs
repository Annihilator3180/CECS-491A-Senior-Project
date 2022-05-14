using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using The6Bits.BitOHealth.DAL.Implementations;
using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class AccountService 
    {
        private IRepositoryAuth<string> _AD;
        private IDBErrors _DBErrors;
        private ISMTPEmailService _EmailService;
        private IConfiguration _config;
        public AccountService(IRepositoryAuth<string> daotype,IDBErrors DbError, 
            ISMTPEmailService EmailService, IConfiguration config)
        {
             _DBErrors= DbError;
            _AD = daotype;
            _EmailService= EmailService;
            _config = config;



        }

        public AccountService(AccountMsSqlDao accountMsSqlDao)
        {
            _AD = accountMsSqlDao;

        }

        public string UsernameExists(string username) 
        {
            string res = _AD.UsernameExists(username);
            if (res == "username exists" || res == "username not found")
            {
                return res;
            }
            else 
            {
                return _DBErrors.DBErrorCheck(int.Parse(res));
            }
        }

        public string UserRole(string username)
        {
            return _AD.UserRole(username);
        }

        public string CheckPassword(string username, string password)
        {
            string res = _AD.CheckPassword(username, password);
            if (res == "credentials found" || res == "not found")
            {
                return res;
            }
            else 
            {
                return _DBErrors.DBErrorCheck(int.Parse(res));
            }
        }

        public string GetEmail(string username)
        {

            string email =  _AD.Read(new User(){Username = username}).Email;
            if (email!="100")
            {
                return email;
            }
            //call error handler
            return _DBErrors.DBErrorCheck(int.Parse(email));
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
                return _DBErrors.DBErrorCheck(int.Parse(res));

            }
        }

        public string SaveActivationCode(string username, DateTime time, string code, string codeType)
        {
            string res =  _AD.SaveActivationCode(username,time,code,codeType);
            if (res == "Saved")
            {
                return "saved";
            }
            else
            {
                return _DBErrors.DBErrorCheck(int.Parse(res));

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
                if ((password.Length >= 8 & password.Length <= 30) & (password.Contains('.') || 
                    password.Contains(',') || password.Contains('!') || password.Contains('@')))
                {
                    password = password.Replace("@", string.Empty).Replace(",", String.Empty).Replace("!", 
                        String.Empty).Replace(".", String.Empty);
                }
                else
                {
                    return false;
                }
                return password.All(char.IsLetterOrDigit) && password.Any(char.IsUpper) &&
                    password.Any(char.IsLower) && password.Any(char.IsDigit);
            }
            catch
            {
                return false;
            }
        }
        public string CreateTempUserName()
        {
            string usernameTest = ",";
            Random rand=new Random();
            usernameTest += rand.Next(100, 10000000).ToString();
            while (true)
            {
                String daoResult = _AD.UsernameExists(usernameTest);
                if (daoResult == "username exists")
                {
                    usernameTest = "," + rand.Next(100, 10000000).ToString();
                }
                else if(daoResult == "username not found")
                {
                    return usernameTest;
                }
                else
                {
                    return _DBErrors.DBErrorCheck(int.Parse(daoResult));
                }
            }
        }

        public string ValidateUsername(string username)
        {
            string usernameTest = username.Replace("@", string.Empty).Replace(",", String.Empty).Replace("!", String.Empty).Replace(".", String.Empty);
            String daoResult = _AD.UsernameExists(username);
            if (!usernameTest.All(Char.IsLetterOrDigit) || username.Length > 16 || username.Length <= 6)
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

        public string MakeUsername(string username)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhijklmnopqrstuvwxyz0123456789.,@!";
            const string firstchar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhijklmnopqrstuvwxyz0123456789.@!";
            var builder = new StringBuilder();
            char c = chars[random.Next(firstchar.Length)];
            builder.Append(c);
            for (int i = 0; i < 10; i++)
            {
                c = chars[random.Next(chars.Length)];
                builder.Append(c);

            }

            return builder.ToString();
        }

        public string MakeNewUserName(string permUsername,string oldUsername)
        {
            String activated = _AD.MakeNewUser(permUsername, oldUsername);
            if (activated == "activated")
            {
                return "activated";
            }
            else
            {
                return _DBErrors.DBErrorCheck(int.Parse(activated));
            }
        }

        public string ActivateUser(string username)
        {
            String activated = _AD.ActivateUser(username);
            if (activated == "activated")
            {
                return "activated";
            }
            else
            {
                return _DBErrors.DBErrorCheck(int.Parse(activated));
            }
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
        
        
        
        public string VerifyTwoMins(string code, string username)
        {
            String res=_AD.VerifyTwoMins(username,code);
            if (res == "1")
            {
                return "valid";
            }
            else if (res == "0")
            {
                return "Code Expired";
            }
            else
            {
               return  _DBErrors.DBErrorCheck(Int32.Parse(res));
            }

        }
        
        
        
        
        
        
        
        
        

        public async Task<String> DeleteCode(string username,string codeType)
        {
            return _AD.DeleteCode(username,codeType);
        }

        public string VerifyAccount(string username)
        {

           string codeInDB = _AD.getCode(username, "Registration");
            if (codeInDB.Length < 10)
            {
                return _DBErrors.DBErrorCheck(int.Parse(codeInDB));
            }
            return codeInDB;
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

            return _DBErrors.DBErrorCheck(int.Parse(res));
        }

        public string CheckFailedAttempts(string username)
        {
            string res = _AD.CheckFailedAttempts(username);
            if (res.Contains("z"))
            {
                return _DBErrors.DBErrorCheck(int.Parse(res.TrimEnd('Z')));

            }
            return res;
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
                return _DBErrors.DBErrorCheck(int.Parse(date));
            }


        }

        public string InsertFailedAttempts(string username)
        {
            string res =  _AD.InsertFailedAttempts(username);
            if (res.Contains("Z")) 
            {
                return _DBErrors.DBErrorCheck(int.Parse(res.TrimEnd('Z')));
            }
            return res;
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
                return _DBErrors.DBErrorCheck(int.Parse(lineschanged));
            }
        }

        public async Task<string> EmailFailed(User user)
        {
            String deletionStatus=_AD.DeleteUnActivated(user);
            if (deletionStatus != "1")
            {
                return _DBErrors.DBErrorCheck(int.Parse(deletionStatus));
            }
            return "True";
        }


        public string VerifyEmail(string username, string email, DateTime now)
        {
            string code = Guid.NewGuid().ToString("N");
            string saveStatus = _AD.SaveActivationCode(username, now, code, "Registration");
            if (saveStatus != "Saved")
            {
                return _DBErrors.DBErrorCheck(int.Parse(saveStatus));
            }

            const string SUBJECT = "Verify your account";
            string Body = "Please use this link to verify your account " +
            "https://bitohealth.com/#/VerifyAccount/" + code+ "/" +  username;
            String EmailStatus = _EmailService.SendEmailNoReply(email, SUBJECT, Body);
            if (EmailStatus != "email sent")
            {
                return EmailStatus;
            }
            return "True";
        }
        public List<Tracking> EmptyDays(List<Tracking> trackedList)
        {
            //last 3 month
            //if day doesnt exist add in with count of 0
            //use map
            DateTime beginning = DateTime.Now.AddMonths(-3).Date;
            List<Tracking> addedEmptyDays = new List<Tracking>();
            int i = 0;
            
            for(DateTime day = beginning; day.Date <= DateTime.Now.Date; day = day.AddDays(1))
            {

                if (addedEmptyDays.Count == 0)
                {
                    if (trackedList.Count > 0 && beginning < trackedList[0].dateTime.Date)
                    {
                        addedEmptyDays.Add(new Tracking(beginning));

                    }
                }
                if (trackedList.Count > i)
                {
                    if (day.Date < trackedList[i].dateTime.Date)
                    {
                        addedEmptyDays.Add(new Tracking(day));
                    }
                    //4/30
                    //trackedlist[0] 4/30
                    else
                    {
                        trackedList[i].date = trackedList[i].dateTime.ToString("MM/dd/yyyy");
                        addedEmptyDays.Add(trackedList[i]);
                        i++;
                    }
                }
                else
                {
                    addedEmptyDays.Add(new Tracking(day));
                }

            }

            return addedEmptyDays;
        }
            


            public List<timeTotal> makeAvgTime(List<timeTotal> time)
        {
            

            foreach (timeTotal occ in time)
            {
                occ.seconds /= occ.occurences;
            }
            return time;
        }

        public List<searchItem> getSearchCount(string type)
        {
            return _AD.getSearchCount(type);
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

            return _DBErrors.DBErrorCheck(int.Parse(res));
        }

        public List<Tracking> GetReg()
        {
            List<Tracking> register = _AD.GetReg();
            foreach (Tracking r in register)
            {
                r.date = r.dateTime.ToString("MM/dd/yyyy");
            }
            return register;
        }

        public List<Tracking> GetLogin(string Type, int months)
        {
            return _AD.GetLogin(Type, months);
        }

        public async Task<List<timeTotal>> AvgTime()
        {
            return await _AD.AvgTime();

        }

        public async Task<List<timeTotal>> BiggestTime()
        {
            return await _AD.BiggestTime();
        }

        public bool MakeView(string view, float time)
        {
            return _AD.MakeView(view, time);
        }

        public bool AddTime(string view, float time)
        {
            return _AD.AddTime(view, time);
        }

        public bool ViewExists(string view)
        {
            return _AD.ViewExists(view) > 0;
        }



        public string DeleteFailedAttempts(string username)
        {
            string res = _AD.DeleteFailedAttempts(username);
            if (res == "1")
            {
                return "1";
            }

            return _DBErrors.DBErrorCheck(int.Parse(res));
        }

        public string AcceptEULA(string username)
        {
            string res = _AD.AcceptEULA(username);
            if (res != "accepted")
            {
                return _DBErrors.DBErrorCheck(int.Parse(res));
            }
            return "accepted";
        }

        public string DeclineEULA(string username)
        {
            string res = _AD.AcceptEULA(username);
            if (res != "declined")
            {
                return _DBErrors.DBErrorCheck(int.Parse(res));
            }
            return "Privacy Declined";
        }
        public string GenerateRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();
            for (int i = 0; i < 10;i++)
            {
                char c = chars[random.Next(chars.Length)];
                builder.Append(c);

            }
            return builder.ToString();
        }





        public string UsernameAndEmailExists(string username, string email)
        {
            string exists = _AD.UsernameAndEmailExists(username, email);
            if (exists == "Email and Username found" || exists == "incorrect")
            {
                return exists;
            }
            return _DBErrors.DBErrorCheck(int.Parse(exists));
        }

        public string ValidateRecoveryAttempts(string username)
        {

            return _AD.ValidateRecoveryAttempts(username);
        }

        public string SendEmail(string email, string subject, string body)
        {
            return _EmailService.SendEmailNoReply(email, subject, body);
        }

        public string UpdateRecoveryAttempts(string username, DateTime dT)
        {
           string ura = _AD.UpdateRecoveryAttempts(username, dT);
           if (ura == "1" || ura == "0")
            {
                return ura;
            }
            return _DBErrors.DBErrorCheck(int.Parse(ura));
        }

        public string VerifySameDay(string username, string code)
        {

            string sd = _AD.VerifySameDay(username, code);
            if (sd == "same day" || sd == "Invalid Reset Link")
            {
                return sd;
            }
            return _DBErrors.DBErrorCheck(int.Parse(sd));

        }
        public string ResetPassword(string password, string username)
        {
            string rp = _AD.ResetPassword(password, username);
            if (rp != "1")
            {
                return _DBErrors.DBErrorCheck(int.Parse(rp));
            }
            return rp;

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

        public string DeleteAccount(string username)
        {
            string deleteStatus = _AD.DeleteAccount(username);
            if (deleteStatus == "0")
            {
                return _DBErrors.DBErrorCheck(int.Parse(deleteStatus));
            }
            else
            {
                return "Account Deleted";
            }


        }

    }




}

