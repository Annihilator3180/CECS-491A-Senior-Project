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
        public async Task<string> CreateReminder(string name, string description, string date, string time, string repeat)
        {


            string token = "";
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

            string username = _authentication.getUsername(token);

            string res = await _RM.CreateReminder(username, name, description, date, time, repeat);

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
        public async Task<string> ViewReminder(string reminderID)
        {

            string token = "";
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

            string username = _authentication.getUsername(token);
            if (reminderID != null)
            {
                return await ViewHelper(username, reminderID);
            }
            else
            {
                return await ViewAllHelper(username);

            }
        }
        public async Task<string> ViewHelper(string username, string reminderID)
        {

            string holder = await _RM.ViewHelper(username);
            string[] subs = holder.Split("ENDING");
            int counter = 1;
            foreach (var sub in subs)
            {
                if (counter == Int32.Parse(reminderID))
                {
                    if (sub == "")
                    {
                        return "Incorrect index";
                    }
                    return sub;
                }
                else
                {
                    counter += 1;
                }
            }
            return "Incorrect index";

        }

        public async Task<string> ViewAllHelper(string username)
        {
            return await _RM.ViewAllHelper(username);
        }

        [HttpPost("ViewAllReminders")]
        public async Task<string> ViewAllReminders()
        {
            string token = "";
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

            string username = _authentication.getUsername(token);

            string s = await _RM.ViewAllReminders(username);
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

        [HttpPost("DeleteReminder")]
        public async Task<string> DeleteReminder(string reminderID)
        {

            string token = "";
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

            string username = _authentication.getUsername(token);

            if (reminderID != null)
            {
                return await _RM.DeleteReminder(username, reminderID);

            }
            else
            {
                return await ViewAllHelper(username);
            }
        }

        [HttpPost("EditReminder")]
        public async Task<string> EditReminder(string reminderID, string name, string description, string date, string time, string repeat)
        {
            string token = "";
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
            string username = _authentication.getUsername(token);

            if (reminderID != null)
            {

                string holder = await _RM.EditReminder(username, reminderID, name, description, date, time, repeat);
                if (holder == "Edit failed")
                {
                    _ = logservice.Log(username, " could not edit reminder", "Info", "Business");
                }
                return holder;
            }
            else
            {
                return await ViewAllHelper(username);
            }
        }
    }
}
