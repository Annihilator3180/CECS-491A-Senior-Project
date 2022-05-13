using System;
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

        public async Task<string> CreateReminder(string username, string name, string description, string date, string time, string repeat)
        {
            if (name == null || name.Length > 100)
            {
                return "Invalid name";
            }
            else if (description != null)
            {
                if (description.Length > 1000)
                {
                    return "Invalid Description";
                }
            }

            int count = await _dao.GetCount(username);
            //big error, if reminder gets deleted then a new one added, the same count is
            //going to be added as reminderID, take last reminder ID and plus one  it
            //so same reminderID is never added
            if (count > 99)
            {
                return "Limit of 100 Reminders Created";
            }
            string res = await _RS.CreateReminder(count, username, name, description, date, time, repeat);
            return res;
        }

        public async Task<string> ViewAllReminders(string username)
        {
            string res = await _RS.ViewAllReminders(username);
            return res;
        }

        public async Task<string> ViewHelper(string username, string reminderID)
        {
            return await _RS.ViewHelper(username, reminderID);
        }

        public async Task<string> ViewAllHelper(string username)
        {
            return await _RS.ViewAllHelper(username);
        }

        public async Task<string> EditReminder(string username, string reminderID, string name, string description, string date, string time, string repeat)
        {
            if (name != null && name.Length > 100)
            {
                return "Invalid name";
            }
            else if (description != null)
            {
                if (description.Length > 1000)
                {
                    return "Invalid Description";
                }
            }
            return await _RS.EditReminder(username, reminderID, name, description, date, time, repeat);
        }

        public async Task<string> DeleteReminder(string username, string reminderID)
        {
            return await _RS.DeleteReminder(username, reminderID);
        }
    }
}
