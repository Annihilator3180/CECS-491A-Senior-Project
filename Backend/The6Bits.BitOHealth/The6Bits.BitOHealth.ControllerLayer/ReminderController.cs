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
            /*
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
            */
            String username = "bossadmin12";
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

        [HttpPost("ViewReminder")]
        public string ViewReminder(string reminderID)
        {
            string username = "bossadmin12";
            if (reminderID != null)
            {
                string holder = ViewHelper(username);
                string res = "";
                string[] subs = holder.Split("ENDING");
                int counter = 1;
                foreach (var sub in subs)
                {
                    if (counter == Int32.Parse(reminderID))
                    {
                        return sub;
                    }
                    else
                    {
                        counter += 1;
                    }
                }
                return "Incorrect index";
            }
            else
            {
                return ViewAllHelper(username);

            }
        }
        public string ViewHelper(string username)
        {
            return _RM.ViewHelper(username);
        }

        public string ViewAllHelper(string username)
        {
            return _RM.ViewAllHelper(username);
        }

        [HttpPost("ViewAllReminders")]
        public string ViewAllReminders(string username)
        {
            string s = _RM.ViewAllReminders(username);
            string[] subs = s.Split('.');
            int counter = 1;
            string holder = "";
            foreach (var sub in subs)
            {
                holder += counter + "." + sub + ".  ";
                counter++;
            }
            int size = holder.Length - 5;
            holder = holder.Remove(size);
            return holder;
        }

        public string DeleteReminder(string reminderID)
        {
            return "";
        }

        public string EditReminder(string reminderID, string name, string description, string date, string time, string repeat)
        {
            string username = "bossadmin12";
            return _RM.EditReminder(username, reminderID, name, description, date, time, repeat);
        }
    }
}
