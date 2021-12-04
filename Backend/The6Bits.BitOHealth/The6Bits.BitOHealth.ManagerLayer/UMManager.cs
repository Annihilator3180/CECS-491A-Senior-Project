using System;
using System.ComponentModel.Design;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer.Contract;
using The6Bits.Logging;
using The6Bits.Logging.Implementations;


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class UMManager
    {
        private IUMService UMS;

        public string CreateAccount(User user)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(user.Username);
            bool emailval = UMS.ValidateEmail(user.Email);
            bool passval = UMS.ValidatePassword(user.Password);
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

            return UMS.CreateAccount(user);
        }

        public string DeleteAccount(string username)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            string response = UMS.DeleteAccount(username);
            return response;
        }

        public string EnableAccount(string username)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            return UMS.EnableAccount(username);
        }

        public string UpdateAccount(User user)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(user.Username);
            if (validation != "username exists")
            {
                return validation;
            }
            if (!UMS.ValidateEmail(user.Email))
            {
                return "invalid email";
            }
            if (!UMS.ValidatePassword(user.Password))
            {
                return "invalid email";
            }

            string response = UMS.UpdateAccount(user);
            
            return response;


        }

        public string DisableAccount(string username)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            string response = UMS.DisableAccount(username);
            
            return response;
        }

    }
}