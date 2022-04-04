using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IReminderDatabase
    {
        string CreateReminder(int count, string username, string name, string description, string date, string time, string repeat);
        public int GetCount(string username);
        string ViewAllReminders(string username);
        string ViewHelper(string username);
        string ViewAllHelper(string username);
        string EditReminder(string username, string reminderID, List<string> edit);
    }
}
