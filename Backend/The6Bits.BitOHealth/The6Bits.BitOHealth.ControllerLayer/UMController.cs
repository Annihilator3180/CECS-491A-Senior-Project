using System;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Authorization.Contract;



namespace The6Bits.BitOHealth.ControllerLayer
{

    public class UMController {
        private UMManager _UMM;
        private LogService logService;
        private string adminUsername;

        public UMController(IRepositoryUM<User> daoType , IAuthorizationService auth ,ILogDal logDao, string adminUsername , string token)
        {
            _UMM = new UMManager(daoType);
            _UMM.auth = auth;
            _UMM.token = token;
            adminUsername = adminUsername;
            logService = new LogService(logDao);
        }

        public string CreateAccount(User user)
        {
            string res = _UMM.CreateAccount(user);

            if (res == "username exists")
            {
                logService.Log(adminUsername, "Account creation-Username Exists", "Info", "Business");
            }
            else if (res == "database error")
            {
                logService.Log(adminUsername, "Create Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Account Creation -"+res, "Info", "Business");
            }

            return res;
        }
        public string DeleteAccount(string username)
        {
            string res = _UMM.DeleteAccount(username);

            if (res != "username exists")
            {
                logService.Log(adminUsername, "Delete Account- new username", "Info", "Business");
            }
            else if (res == "database error")
            {
                logService.Log(adminUsername, "Delete Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Delete Account -"+res, "Info", "Business");
            }

            return res;
        }
        public string UpdateAccount(User user)
        {
            string res = _UMM.UpdateAccount(user);

            if (res != "username exists")
            {
                logService.Log(adminUsername, "Update Account- new username", "Info", "Business");
            }
            else if (res == "database error")
            {
                logService.Log(adminUsername, "Update Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Update Account -" + res, "Info", "Business");
            }
            return res;
        }
        public string EnableAccount(string username)
        {
            string res = _UMM.EnableAccount(username);
            if (res != "username exists")
            {
                logService.Log(adminUsername, "Enabel Account - new username", "Info", "Business");
            }
            else if (res == "database error")
            {
                logService.Log(adminUsername, "Enable Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Enable Account -" + res, "Info", "Business");
            }
            return res;
        }
        public string DisableAccount(string username)
        {
            string res = _UMM.DisableAccount(username);
            if (res != "username exists")
            {
                logService.Log(adminUsername, "Disable Account- new username", "Info", "Business");
            }
            else if (res == "database error")
             {
                logService.Log(adminUsername, "Disable Account-Database Error", "Error", "Data Store");
            }
            else
            {
                logService.Log(adminUsername, "Disable Account -" + res, "Info", "Business");
            }
            return res;
        }


    }
}