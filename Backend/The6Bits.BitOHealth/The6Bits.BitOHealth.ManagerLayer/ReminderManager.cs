using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class ReminderManager
    {
        private ReminderService _RS;
        public ReminderManager(IReminderDatabase dao)
        {
            _RS = new ReminderService(dao);
        }
        public bool CreateReminder(string username, string name, string description, string date, string time, string repeat)
        {
            //catch error here

            bool dao = _RS.CreateReminder(username, name, description, date, time, repeat);

            return dao;
        }
    }
}
