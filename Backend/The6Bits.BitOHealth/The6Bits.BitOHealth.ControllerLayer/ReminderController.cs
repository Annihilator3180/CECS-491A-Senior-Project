using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ControllerLayer
{

    [Route("Reminder")]
    public class ReminderController : ControllerBase
    {
        private ReminderManager _RM;
        private IReminderDatabase _dao;
        private IAuthenticationService _authentication;
        private LogService logservice;
        private bool isValid;

        public ReminderController(IReminderDatabase dao, IAuthenticationService authentication, ILogDal logDal, IDBErrors db)
        {
            _dao = dao;
            _authentication = authentication;
            logservice = new LogService(logDal);
            _RM = new ReminderManager(dao, db);
        }

        [HttpPost("CreateReminder")]
        public string CreateReminder(string name, string description, string date, string time, string repeat)
        {
            String token = "";
            try
            {
                token = Request.Cookies["token"];
            }
            catch
            {
                return "No token";
            }
            isValid = _authentication.ValidateToken(token);
            if (!isValid)
            {
                _ = logservice.Log("None", "Invalid Token - Create Reminder", "Info", "Business");
                return "Invalid Token";
            }

            String username = _authentication.getUsername(token);
            string res = _RM.CreateReminder(username, name, description, date, time, repeat);

            if (res.Contains("Database"))
            {
                _ = logservice.Log(username, "Reminder Created" + res, "DataStore", "Error");
                return res;
            }
            else if (res.Contains("NOT"))
            {
                _ = logservice.Log(username, "Reminder NOT Created" + res, "DataStore", "Not Created");
                return res;
            }

            _ = logservice.Log(username, "Reminder Created", "Info", "Business");

            return res;
        }
    }
}
