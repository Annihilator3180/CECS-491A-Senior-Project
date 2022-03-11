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

        public string CreateReminder(string username, string name, string description, string date, string time, string repeat)
        {
            string res = _RS.CreateReminder(username, name, description, date, time, repeat);

            if (res.Contains("Reminder")){
                return res;
            }
            return _DBErrors.DBErrorCheck(int.Parse(res));

        }
    }
}
