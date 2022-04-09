﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class ReminderManager
    {
        private ReminderService _RS;
        private IReminderDatabase _dao;
        private IDBErrors _DBErrors;
        public ReminderManager(IReminderDatabase dao, IDBErrors db)
        {
            _DBErrors = db;
            _dao = dao;
            _RS = new ReminderService(dao);
        }

        public string CreateReminder(string username, string name, string description, string date, string time, string repeat)
        {
            string res = _RS.CreateReminder(username, name, description, date, time, repeat);

            if (res.Contains("Reminder")){
                return res;
            }
            return _DBErrors.DBErrorCheck(int.Parse(res));

        }
        public string ViewAllReminders(string username)
        {
            string res = _RS.ViewAllReminders(username);
            return res;
        }

        public string ViewHelper(string username)
        {
            return _RS.ViewHelper(username);
        }

        public string ViewAllHelper(string username)
        {
            return _RS.ViewAllHelper(username);
        }

        public string EditReminder(string username, string reminderID, string name, string description, string date, string time, string repeat)
        {
            return _RS.EditReminder(username, reminderID, name, description, date, time, repeat);
        }

        public string DeleteReminder(string username, string reminderID)
        {
            return _RS.DeleteReminder(username, reminderID);
        }
    }
}
