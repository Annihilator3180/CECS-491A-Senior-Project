using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IReminderDatabase
    {
        string CreateReminder(int count, string username, string name, string description, string date, string time, string repeat);
        public int GetCount(string username);
        string ViewAllReminders(string username);
        string ViewHelper(string username);
        string ViewAllHelper(string username);
        string EditReminder(string username, string reminderID, string name, string description, string date, string time, string repeat);
        string DeleteReminder(string username, string reminderID);
        public List<string> EditHelper(string username, string reminderID);
    }
}
