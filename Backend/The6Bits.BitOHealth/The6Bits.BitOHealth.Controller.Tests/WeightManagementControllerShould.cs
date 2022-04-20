using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FoodAPI;
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
    public class WeightManagementControllerShould : ControllerBase
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
        public WeightManagementController WeightManagementContext()
        {
            testingInfo();
            _dao = new WeightManagementMsSqlDao(_conn);
            WeightManagementController controller = new WeightManagementController(new WeightManagementMsSqlDao(_conn), new JWTAuthenticationService(_keyPath),
                new SQLLogDAO(),new MsSqlDerrorService(),new EdamamAPIService<Parsed>(new HttpClient(), new EdamamConfig(){AppId = _id, AppKey = _key }), new WeightManagementWindowsDao("%USERPROFILE%\\Pictures\\TestImages\\"));

            DefaultHttpContext context = new DefaultHttpContext();
            context.Request.Headers.Add("Authorization", _testingToken);
            controller.ControllerContext.HttpContext = context;
            return controller;
        }
        public WeightManagementController badContext()
        {
            testingInfo();
            WeightManagementController controller = new WeightManagementController(new WeightManagementMsSqlDao(_conn), new JWTAuthenticationService(_keyPath),
                new SQLLogDAO(), new MsSqlDerrorService(), new EdamamAPIService<Parsed>(new HttpClient(), new EdamamConfig() { AppId = _id, AppKey = _key }), new WeightManagementWindowsDao("%USERPROFILE%\\Pictures\\TestImages\\"));

            DefaultHttpContext context = new DefaultHttpContext();
            context.Request.Headers.Add("Authorization", "");
            controller.ControllerContext.HttpContext = context;
            return controller;
        }


        [Fact]
        public async void CreateGoalShould()
        {
            WeightManagementController weightManagementController = WeightManagementContext();
            ActionResult res = await weightManagementController.CreateGoal(new GoalWeightModel(122, DateTime.UtcNow.AddMonths(1), 2500, 200));

            Assert.IsType<OkObjectResult>(res);

            await weightManagementController.DeleteGoal();
        }


        [Fact]
        public async void SearchFoodShould()
        {
            WeightManagementController weightManagementController = WeightManagementContext();
            ActionResult res = await weightManagementController.SearchFood("pizza");

            Assert.IsType<OkObjectResult>(res);

        }


        [Fact]
        public async void UpdateGoalShould()
        {
            WeightManagementController weightManagementController = WeightManagementContext();
            ActionResult res = await weightManagementController.CreateGoal(new GoalWeightModel(122, DateTime.UtcNow.AddMonths(1), 2500, 200));
            ActionResult update = await weightManagementController.UpdateGoal(new GoalWeightModel(1, DateTime.UtcNow.AddMonths(1), 1, 1));

            Assert.IsType<OkObjectResult>(res);

            await weightManagementController.DeleteGoal();
        }



        [Fact]
        public async void ReadGoalShould()
        {
            WeightManagementController weightManagementController = WeightManagementContext();
            ActionResult res = await weightManagementController.CreateGoal(new GoalWeightModel(122, DateTime.UtcNow.AddMonths(1), 2500, 200));
            ActionResult update = await weightManagementController.ReadGoal();

            Assert.IsType<OkObjectResult>(res);

            await weightManagementController.DeleteGoal();
        }


        [Fact]
        public async void StoreFoodLogShould()
        {
            WeightManagementController weightManagementController = WeightManagementContext();
            ActionResult res = await weightManagementController.CreateGoal(new GoalWeightModel(122, DateTime.UtcNow.AddMonths(1), 2500, 200));
            ActionResult update = await weightManagementController.ReadGoal();

            Assert.IsType<OkObjectResult>(res);

            await weightManagementController.DeleteGoal();
        }

    }
}
