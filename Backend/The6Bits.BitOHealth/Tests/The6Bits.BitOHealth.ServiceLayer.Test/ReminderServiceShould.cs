using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.Models;
using Xunit;



namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class ReminderServiceShould : TestsBase
    {
        private ReminderService _reminderService;
        private IReminderDatabase _dao;
        private string username = "bossadmin12";

        public ReminderServiceShould()
        {
            _dao = new ReminderMsSqlDao(conn);
            _reminderService = new ReminderService(_dao);
        }

        [Fact]
        public async void viewAllRemindersT()
        {
            string should = await _reminderService.ViewAllReminders(username);
            Assert.NotNull(should);
        }

        [Fact]
        public async void editReminder()
        {
            string name = "HW", description = "327 hw", date = "03-16-2022", time = "11:59 pm", repeat = "weekly";
            await _reminderService.CreateReminder(0, username, name, description, date, time, repeat);
            string holder = await _dao.EditReminder(username, "1", name, description, date, time, repeat);
            Assert.Equal("Reminder Edited", holder);
            await _dao.DeleteReminder(username, "1");

        }

        [Fact]
        public async void createReminder()
        {
            string name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            Thread.Sleep(5000);
            string should = await _reminderService.CreateReminder(0,username, name, description, date, time, repeat);
            Assert.Equal("Reminder Created", should);
            await _reminderService.DeleteReminder(username, "1");
        }

        [Fact]
        public async void deleteReminder()
        {
            string name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            await _reminderService.CreateReminder(0,username, name, description, date, time, repeat);
            string holder = await _dao.DeleteReminder(username, "1");
            Assert.Equal("Reminder Deleted", holder);
        }

    }
}
