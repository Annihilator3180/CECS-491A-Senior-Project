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
            ILogService logService = new SQLLogService();
            if (validation != "username exists")
            {
                logService.Log(user.Username, "username does not exist", "Informational", "Service");
                return validation;
            }
            if (!UMS.ValidateEmail(user.Email))
            {
                logService.Log(user.Username, "email input incorrect", "Informational", "Service");
                return "invalid email";
            }
            if (!UMS.ValidatePassword(user.Password))
            {
                logService.Log(user.Username, "password input incorrect", "Informational", "Service");
                return "invalid email";
            }

            string response = UMS.CreateAccount(user);
            if (response == "database error")
            {
                logService.Log(user.Username, response, "Error", "Data Store");
            }
            else
            {
                logService.Log(user.Username, response, "Informational", "Business");
            }
            return response;
        }

        public string DeleteAccount(string username)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(username);
            ILogService logService = new SQLLogService();
            if (validation != "username exists")
            {
                logService.Log(username, "username does not exist", "Informational", "Service");
                return validation;
            }
            string response = UMS.DeleteAccount(username);
            if (response == "database error")
            {
                logService.Log(username, response, "Error", "Data Store");
            }
            else
            {
                logService.Log(username, response, "Informational", "Business");
            }
            return response;
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

        public string UpdateAccount(User user)
        {
            UMS = new UMService(new SqlUMDAO<User>());
            string validation = UMS.ValidateUsername(user.Username);
            ILogService logService = new SQLLogService();
            if (validation != "username exists")
            {
                logService.Log(user.Username, "username does not exist", "Informational", "Service");
                return validation;
            }
            if (!UMS.ValidateEmail(user.Email))
            {
                logService.Log(user.Username, "email input incorrect", "Informational", "Service");
                return "invalid email";
            }
            if (!UMS.ValidatePassword(user.Password))
            {
                logService.Log(user.Username, "password input incorrect", "Informational", "Service");
                return "invalid email";
            }

            string response = UMS.UpdateAccount(user);
            if (response == "database error")
            {
                logService.Log(user.Username, response, "Error", "Data Store");
            }
            else
            {
                logService.Log(user.Username, response, "Informational", "Business");
            }
            return response;


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
            string response = UMS.DisableAccount(username);
            if (response == "database error")
            {
                logService.Log(username, response, "Error", "Data Store");
            }
            else
            {
                logService.Log(username, response, "Informational", "Business");
            }
            return response;
        }

    }
}