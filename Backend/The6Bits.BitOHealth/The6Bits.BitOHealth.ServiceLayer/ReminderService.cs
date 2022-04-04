using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class ReminderService
    {
        public IReminderDatabase _Rdao;

        public ReminderService(IReminderDatabase dao)
        {
            _Rdao = dao;
        }
        public string CreateReminder(string username, string name, string description, string date, string time, string repeat)
        {

            int count = _Rdao.GetCount(username);
            return _Rdao.CreateReminder(count, username, name, description, date, time, repeat);
        }

        public string ViewAllReminders(string username)
        {
            return _Rdao.ViewAllReminders(username);
        }

        public string ViewHelper(string username)
        {
            return _Rdao.ViewHelper(username);
        }

        public string ViewAllHelper(string username)
        {
            string s = _Rdao.ViewAllHelper(username);
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

        public string EditReminder(string username, string reminderID, string name, string description, string date, string time, string repeat)
        {
            List<string> edit = new List<string> { name, description, date, time, repeat };
            return _Rdao.EditReminder(username, reminderID, edit);
        }
    }
}
