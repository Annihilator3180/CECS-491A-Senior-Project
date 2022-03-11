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
        public IReminderDatabase _dao;
        public ReminderService(IReminderDatabase dao)
        {
            _dao = dao;
        }
        public bool CreateReminder(string username, string name, string description, string date, string time, string repeat)
        {
            bool dao = _dao.CreateReminder(username, name, description, date, time, repeat);

            return dao;
        }
    }
}
