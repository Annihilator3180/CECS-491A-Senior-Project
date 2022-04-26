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
        Task<string> CreateReminder(int count, string username, string name, string description, string date, string time, string repeat);
        Task<int> GetCount(string username);
        Task<string> ViewAllReminders(string username);
        Task<string> ViewHelper(string username, string reminderID);
        Task<string> ViewAllHelper(string username);
        Task<string> EditReminder(string username, string reminderID, string name, string description, string date, string time, string repeat);
        Task<string> DeleteReminder(string username, string reminderID);
        public List<string> EditHelper(string username, string reminderID);
    }
}
