using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IReminderDatabase
    {
        bool CreateReminder(string username, string reminderName, string description, string date, string time, string repeat);
    }
}
