using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.IdentityModel.Tokens;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.Models.WeightManagement;
using The6Bits.Logging.DAL.Implementations;
using Xunit;

namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class WeightManagementServiceShould : TestsBase
    {

        private readonly WeightManagementService _service;
        public WeightManagementServiceShould()
        {
            _service = new WeightManagementService(new WeightManagementMsSqlDao(conn), new SQLLogDAO(),new WeightManagementWindowsDao("%USERPROFILE%\\Pictures\\TestImages\\"));
        }

        [Theory]
        [MemberData(nameof(LoadGoalWeightJson))]

        public async void CreateGoalShould(GoalWeightModel goal)
        {
            await _service.CreateGoal(goal, "testuser");
            IWeightManagerResponse res = await _service.ReadGoal("testuser");
            GoalWeightModel readResult = (GoalWeightModel) res.Result;


            Assert.Equal(goal.CurrentWeight,readResult.CurrentWeight);

            await _service.DeleteGoal("testuser");

        }


        [Theory]
        [MemberData(nameof(LoadGoalWeightJson))]

        public async void DeleteGoalShould(GoalWeightModel goal)
        {
            await _service.CreateGoal(goal, "testuser");
            await _service.DeleteGoal("testuser");
            IWeightManagerResponse res = await _service.ReadGoal("testuser");
            GoalWeightModel readResult = (GoalWeightModel)res.Result;


            Assert.Null(readResult.GoalWeight);

            await _service.DeleteGoal("testuser");

        }


        [Theory]
        [MemberData(nameof(LoadGoalWeightJson))]

        public async void UpdateGoalShould(GoalWeightModel goal)
        {
            await _service.CreateGoal(goal, "testuser");
            await _service.UpdateGoal(new GoalWeightModel(0,DateTime.UtcNow,0,0),"testuser");
            IWeightManagerResponse res = await _service.ReadGoal("testuser");
            GoalWeightModel readResult = (GoalWeightModel)res.Result;


            Assert.NotEqual(goal.GoalWeight,readResult.GoalWeight);

            await _service.DeleteGoal("testuser");

        }

        [Theory]
        [MemberData(nameof(LoadFoodJson))]

        public async void StoreFoodLogShould(FoodModel food)
        {
            await _service.StoreFoodLog(food, "testuser");
            IWeightManagerResponse res = await _service.GetFoodLogs("testuser");
            IEnumerable < FoodModel > readResult = (IEnumerable<FoodModel>)res.Result;


            Assert.Equal(readResult.First().Calories, food.Calories);



            await _service.DeleteFoodLog((int)readResult.First().Id, "testuser");

        }


        [Theory]
        [MemberData(nameof(LoadFoodJson))]
        public async void GetFoodLogsAfterShould(FoodModel food)
        {
            await _service.StoreFoodLog(food, "testuser");



            IWeightManagerResponse res = await _service.GetFoodLogsAfter(new DateTime(1990, 01, 01), "testuser");
            FoodModel foodModel = ((IEnumerable<FoodModel>)res.Result).First();




            Assert.Equal(foodModel.Calories, food.Calories);



            await _service.DeleteFoodLog((int)foodModel.Id, "testuser");


        }



        [Theory]
        [InlineData("dasdas", "testuser")]
        [InlineData(@"C:/asdas", "testuser")]
        [InlineData(@"D://asdas", "testuser")]

        public async void SaveImagePathShould(string path, string username)
        {
            await _service.SaveImagePath(path,  username);
            IWeightManagerResponse res = await _service.GetAllImageIds(username);

            IEnumerable<string> idEnum = ((IEnumerable<string>)res.Result);



            //SEE IF GOT ADDED
            Assert.False(idEnum.IsNullOrEmpty());



            await _service.DeleteImagePath(Int32.Parse(idEnum.First()), username);

        }


        [Theory]
        [InlineData("dasdas", "testuser")]
        [InlineData(@"C:/asdas", "testuser")]
        [InlineData(@"D://asdas", "testuser")]
        public async void GetImageShould(string path, string username)
        {
            await _service.SaveImagePath(path,  username);
            IWeightManagerResponse res = await _service.GetAllImageIds(username);

            IEnumerable<string> idEnum = ((IEnumerable<string>)res.Result);
            IWeightManagerResponse get = await _service.GetImage(Int32.Parse(idEnum.First()), username);
            Assert.Equal(get.Result, path);


            await _service.DeleteImagePath(Int32.Parse(idEnum.First()), username);

        }



        [Theory]
        [InlineData("dummy.txt")]
        [InlineData("dasjkdhaskdasm.txt")]
        public async void SaveImageShould(string filename)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", filename);
            IWeightManagerResponse res = await _service.SaveImage(file, "testuser");




            Assert.True(System.IO.File.Exists((string)res.Result));




            await _service.DeleteImage(((string)res.Result), "testuser");
        }




        [Theory]
        [InlineData("dummy.txt")]
        [InlineData("dasjkdhaskdasm.txt")]
        public async void DeleteImageShould(string filename)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", filename);
            IWeightManagerResponse res = await _service.SaveImage(file, "testuser");
            await _service.DeleteImage(((string)res.Result), "testuser");




            Assert.False(System.IO.File.Exists((string)res.Result));

        }







        public static IEnumerable<object[]> LoadGoalWeightJson()
        {

            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            string p = di.Parent.Parent.Parent.Parent.ToString();
            string filePath = Path.GetFullPath(p + @"/TestData/WeightGoalModelTestData.json");
            var json = File.ReadAllText(filePath);
            var foods = JsonSerializer.Deserialize<List<GoalWeightModel>>(json);
            var objectList = new List<object[]>();
            foreach (var data in foods)
            {
                objectList.Add(new object[] { data });
            }
            return objectList;

        }


        public static IEnumerable<object[]> LoadFoodJson()
        {

            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            string p = di.Parent.Parent.Parent.Parent.ToString();
            string filePath = Path.GetFullPath(p + @"/TestData/FoodModelTestData.json");
            var json = File.ReadAllText(filePath);
            var foods = JsonSerializer.Deserialize<List<FoodModel>>(json);
            var objectList = new List<object[]>();
            foreach (var data in foods)
            {
                objectList.Add(new object[] { data });
            }
            return objectList;

        }


    }
}
