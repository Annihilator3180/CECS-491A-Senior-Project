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

        public UMController(IRepositoryUM<User> daoType , IAuthorizationService auth ,ILogDal logDao, string adminUsername , string token)
        {
            _UMM = new UMManager(daoType);
            _UMM.auth = auth;
            _UMM.token = token;
            logService = new LogService(logDao);
        }

        public string CreateAccount(User user)
        {
            string res = _UMM.CreateAccount(user);

            if (res == "username exists")
            {
                logService.Log(user.Username, "Username Already Used", "Info", "Data");
            }
            else
            {
                logService.Log(user.Username, res, "Info", "Server");
            }

            return res;
        }
        public string DeleteAccount(string username)
        {
            string res = _UMM.DeleteAccount(username);

            if (res != "username exists")
            {
                logService.Log(username, "Username Does Not Exist", "Info", "Data");
            }
            else
            {
                logService.Log(username, res, "Info", "Server");
            }

            return res;
        }
        public string UpdateAccount(User user)
        {
            string res = _UMM.UpdateAccount(user);

            if (res != "username exists")
            {
                logService.Log(user.Username, "Username Does Not Exist", "Info", "Data");
            }
            else
            {
                logService.Log(user.Username, res, "Info", "Server");
            }
            return res;
        }
        public string EnableAccount(string username)
        {
            string res = _UMM.EnableAccount(username);
            if (res != "username exists")
            {
                logService.Log(username, "Username Does Not Exist", "Info", "Data");
            }
            else
            {
                logService.Log(username, res, "Info", "Server");
            }
            return res;
        }
        public string DisableAccount(string username)
        {
            string res = _UMM.DisableAccount(username);
            if (res != "username exists")
            {
                logService.Log(username, "Username Does Not Exist", "Info", "Data");
            }
            else
            {
                logService.Log(username, res, "Info", "Server");
            }
            return res;
        }


    }
}