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
        public async Task<string> CreateReminder(int count, string username, string name, string description, string date, string time, string repeat)
        {

            return await _Rdao.CreateReminder(count, username, name, (description + "."), date, time, repeat);

        }

        public async Task<string> ViewAllReminders(string username)
        {
            return await _Rdao.ViewAllReminders(username);
        }

        public async Task<string> ViewHelper(string username, string reminderID)
        {
            return await _Rdao.ViewHelper(username, reminderID);
        }

        public async Task<string> ViewAllHelper(string username)
        {
            string s = await _Rdao.ViewAllHelper(username);
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

        public async Task<string> EditReminder(string username, string reminderID, string name, string description, string date, string time, string repeat)
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
                    if (i == 1)
                    {
                        edit.Add(input.ElementAt(i) + ".");
                    }
                    else
                    {
                        edit.Add(input.ElementAt(i));

                    }
                }
            }
            return await _Rdao.EditReminder(username, reminderID, edit.ElementAt(0), (edit.ElementAt(1)), edit.ElementAt(2), edit.ElementAt(3), edit.ElementAt(4));
        }

        public async Task<string> DeleteReminder(string username, string reminderID)
        {
            return await _Rdao.DeleteReminder(username, reminderID);
        }
    }
}
