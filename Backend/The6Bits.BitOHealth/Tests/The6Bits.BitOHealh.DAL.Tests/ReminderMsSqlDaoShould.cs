using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Contract;
using System.Threading;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class ReminderMsSqlDaoShould : TestsBase
    {
        private IReminderDatabase _dao;
        public static bool test1;
        public static bool test2;
        public static bool test3;
        public static bool test4;

        public ReminderMsSqlDaoShould()
        {
            _dao = new ReminderMsSqlDao(conn);
        }

        [Fact]
        public async void EditReminder()
        {
            string username = "bossadmin12", name = "HW", description = "327 hw", date = "03-16-2022", time = "11:59 pm", repeat = "weekly";
            await _dao.CreateReminder(1, username, name, description, date, time, repeat);
            string holder = await _dao.EditReminder(username, "1", name, description, date, time, repeat);
            Assert.Equal("Reminder Edited", holder);
            await _dao.DeleteReminder(username, "1");

        }

        [Fact]
        public async void ViewReminder()
        {
            string username = "bossadmin12", name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            await _dao.CreateReminder(1, username, name, description, date, time, repeat);
            string holder = await _dao.ViewHelper(username, "1");
            Assert.Equal("HW Do 342 hw 03-16-2022 04:00 pm weekly", holder);
            await _dao.DeleteReminder(username, "1");

        }

        [Fact]
        public async void DeleteReminder()
        {
            string username = "bossadmin12", name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            await _dao.CreateReminder(1, username, name, description, date, time, repeat);
            string holder = await _dao.DeleteReminder(username, "1");
            Assert.Equal("Reminder Deleted", holder);
        }


        [Fact]
        public async void CreateReminder()
        {
            Thread.Sleep(1000);
            string username = "bossadmin12", name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            string holder = await _dao.CreateReminder(1, username, name, description, date, time, repeat);
            Assert.Equal("Reminder Created", holder);
            await _dao.DeleteReminder(username, "1");

        }

    }
}
