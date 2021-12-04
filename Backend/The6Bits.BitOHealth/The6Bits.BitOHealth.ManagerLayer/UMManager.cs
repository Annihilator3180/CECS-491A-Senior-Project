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
            if (!UMS.ValidateEmail(user.Email))
            {
                return "Invalid Email";
            }
            if (UMS.ValidatePassword(user.Password))
            {
                return "Invalid Password";
            }
            string validation = UMS.ValidateUsername(user.Username);
            return validation != "new username" ? validation : UMS.CreateAccount(user);
        }

        public string DeleteAccount(string username)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            string ret = UMS.DeleteAccount(username);
            return ret;
        }

        public string EnableAccount(string username)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                ILogService logService = new SQLLogService();
                logService.Log(username, "username does not exist", "Informational", "Service");
                return validation;
            }
            return UMS.EnableAccount(username);
        }

        public string UpdateAccount(string username)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                ILogService logService = new SQLLogService();
                logService.Log(username, "username does not exist", "Informational", "Service");
                return validation;
            }
            return UMS.DeleteAccount(username);


        }

        public string DisableAccount(string username)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(username);
            ILogService logService = new SQLLogService();
            if (validation != "username exists")
            {
                logService.Log(username, "username does not exist", "Informational", "Service");
                return validation;
            }

            string disable = UMS.DisableAccount(username);
            logService.Log(username, disable, "Informational", "Service");
            return disable;
        }

    }
}