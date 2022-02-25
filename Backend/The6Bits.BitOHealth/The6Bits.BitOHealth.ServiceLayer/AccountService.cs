﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
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
            string sender = "test@bitohealth.com";
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "bitohealth.com",
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential("username", "password"),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(sender, email, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                return "email failed to send";
            }
            return "email sent";
        }



    }
}
