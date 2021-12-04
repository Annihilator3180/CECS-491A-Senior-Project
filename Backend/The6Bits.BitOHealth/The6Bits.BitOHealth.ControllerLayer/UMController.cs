using System;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging;
using The6Bits.Logging.Implementations;


namespace The6Bits.BitOHealth.ControllerLayer
{

    public class UMController 
    {

        public string CreateAccount(User user)
        {
            UMManager UMM = new UMManager();
            string res =  UMM.CreateAccount(user);

            ILogService logService = new SQLLogService(new SQLLogDAO());

            if (res == "username exists")
            {
                logService.Log(user.Username, "Username Already Used", "Info", "Data");
            }
            else if (res == "database error")
            {
                logService.Log(user.Username, "Database Timeout", "Error", "Data Store");

            }
            else
            {
                logService.Log(user.Username, res, "Info", "Server");
            }

            return res;
        }
        public string DeleteAccount(string username)
        {
            UMManager UMM = new UMManager();
            string res = UMM.DeleteAccount(username);
            ILogService logService = new SQLLogService(new SQLLogDAO());

            if (res != "username exists")
            {
                logService.Log(username, "Username Already Used", "Info", "Data");
            }
            else if (res == "database error")
            {
                logService.Log(username, "Database Timeout", "Error", "Data Store");

            }
            else
            {
                logService.Log(username, res, "Info", "Server");
            }

            return UMM.DeleteAccount(username);
        }
        public string UpdateAccount(User user)
        {
            UMManager UMM = new UMManager();
            string res = UMM.UpdateAccount(user);
            ILogService logService = new SQLLogService(new SQLLogDAO());

            if (res != "username exists")
            {
                logService.Log(user.Username, "Username Already Used", "Info", "Data");
            }
            else if (res == "database error")
            {
                logService.Log(user.Username, "Database Timeout", "Error", "Data Store");

            }
            else
            {
                logService.Log(user.Username, res, "Info", "Server");
            }
            return res;
        }
        public string EnableAccount(string username)
        {
            UMManager UMM = new UMManager();
            ILogService logService = new SQLLogService(new SQLLogDAO());
            string res = UMM.EnableAccount(username);
            if (res != "username exists")
            {
                logService.Log(username, "Username Already Used", "Info", "Data");
            }
            else if (res == "database error")
            {
                logService.Log(username, "Database Timeout", "Error", "Data Store");

            }
            else
            {
                logService.Log(username, res, "Info", "Server");
            }
            return res;
        }
        public string DisableAccount(string username)
        {
            UMManager UMM = new UMManager();
            ILogService logService = new SQLLogService(new SQLLogDAO());
            string res = UMM.DisableAccount(username);
            if (res != "username exists")
            {
                logService.Log(username, "Username Already Used", "Info", "Data");
            }
            else if (res == "database error")
            {
                logService.Log(username, "Database Timeout", "Error", "Data Store");

            }
            else
            {
                logService.Log(username, res, "Info", "Server");
            }
            return res;
        }

    }
}