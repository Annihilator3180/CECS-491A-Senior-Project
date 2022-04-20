using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class CreateReminderShould: TestsBase
    {
        private IReminderDatabase _dao;
        
        public CreateReminderShould()
        {
            _dao = new ReminderMsSqlDao(conn);
        }

        [Fact]
        public void createR()
        {
            string username = "bossadmin12", name = "HW",  description = "Do 342 hw",  date = "03-16-2022",  time = "04:00 pm",  repeat = "weekly";
           // string holder = _dao.CreateReminder(username, name, description, date, time, repeat);
            Assert.Equal("Reminder Created", holder);
        }
    }
}
