using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

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
            //big error, if reminder gets deleted then a new one added, the same count is
            //going to be added as reminderID, take last reminder ID and plus one  it
            //so same reminderID is never added
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
            List<string> old = _Rdao.EditHelper(username, reminderID);
            List<string> input = new List<string> { name, description, date, time, repeat };
            List<string> edit = new List<string>();
            for (int i = 0; i < old.Count; i++)
            {
                if (input.ElementAt(i) == null)
                {
                    edit.Add(old[i]);
                }
                else
                {
                    edit.Add(input.ElementAt(i));
                }
            }
            return _Rdao.EditReminder(username, reminderID, edit.ElementAt(0), edit.ElementAt(1), edit.ElementAt(2), edit.ElementAt(3), edit.ElementAt(4));
        }

        public string DeleteReminder(string username, string reminderID)
        {
            return _Rdao.DeleteReminder(username, reminderID);
        }
    }
}
