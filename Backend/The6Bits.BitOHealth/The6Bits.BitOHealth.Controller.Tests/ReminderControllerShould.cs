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
        private WeightManagementMsSqlDao _dao;

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

        }



        public async void createReminder()
        {

        }
    }
}
