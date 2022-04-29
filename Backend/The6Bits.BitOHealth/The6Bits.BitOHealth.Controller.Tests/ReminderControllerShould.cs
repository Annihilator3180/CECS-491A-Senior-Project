using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.ControllerLayer;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;
using The6Bits.Logging.DAL.Implementations;
using Xunit;
namespace The6Bits.BitOHealth.Controller.Tests
{
    public class ReminderControllerShould : ControllerBase
    {
        private string _id;
        private string _key;
        private string _conn;
        private string _keyPath;
        private string _testingToken;
        private ReminderMsSqlDao _dao;
        private string username = "bossadmin12";

        public void testingInfo()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(System.IO.Path.Combine(AppContext.BaseDirectory, @"..\..\..\"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("secrets.json")
                .Build();
            _conn = configuration.GetConnectionString("DefaultConnection");
            _keyPath = configuration.GetSection("PK")["jwt"];
            _testingToken = configuration.GetSection("PK")["TestingToken"];
            _id = configuration.GetSection("Edamam")["Id"];
            _key = configuration.GetSection("Edamam")["Key"];
        }


        public ReminderController reminderContext()
        {
            testingInfo();
            _dao = new ReminderMsSqlDao(_conn);
            ReminderController controller = new ReminderController(new ReminderMsSqlDao(_conn), new JWTAuthenticationService(_keyPath),
                new SQLLogDAO(), new MsSqlDerrorService());
            DefaultHttpContext context = new DefaultHttpContext();
            context.Request.Headers.Add("Authorization", _testingToken);
            controller.ControllerContext.HttpContext = context;
            return controller;

        }

        [Fact]
        public async void createReminder()
        {
            string name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            ReminderController reminderController = reminderContext();
            string res = await reminderController.CreateReminder(name, description, date, time, repeat);
            Assert.Equal("Reminder Created", res);
            await reminderController.DeleteReminder("1");
        }

        [Fact]
        public async void viewReminder()
        {
            string name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            ReminderController reminderController = reminderContext();
            await reminderController.CreateReminder(name, description, date, time, repeat);
            string res = await reminderController.ViewReminder("1");
            Assert.NotNull(res);
            await reminderController.DeleteReminder("1");
        }

        [Fact]
        public async void editReminder()
        {
            string name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            ReminderController reminderController = reminderContext();
            await reminderController.CreateReminder(name, description, date, time, repeat);
            string res = await reminderController.EditReminder("1", name, description, date, time, repeat);
            Assert.Equal("Reminder Edited", res);
            await reminderController.DeleteReminder("1");
        }

        [Fact]
        public async void deleteReminder()
        {
            string name = "HW", description = "Do 342 hw", date = "03-16-2022", time = "04:00 pm", repeat = "weekly";
            ReminderController reminderController = reminderContext();
            string res = await reminderController.CreateReminder(name, description, date, time, repeat);
            Assert.Equal("Reminder Created", res);
            await reminderController.DeleteReminder("1");
        }
    }
}
