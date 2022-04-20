using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.Models.WeightManagement;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class WeightManagementMsSqlDaoShould : TestsBase
    {

        private IRepositoryWeightManagementDao<IWeightManagerResponse> _dao;
        public WeightManagementMsSqlDaoShould()
        {
            _dao = new WeightManagementMsSqlDao(conn);
        }




        [Theory]
        [MemberData(nameof(LoadGoalWeightJson))]
        public async void CreateTest(GoalWeightModel goalWeightModel)
        {

            await _dao.Delete("tester");
            await _dao.Create(goalWeightModel, "tester");
            IWeightManagerResponse res = await _dao.Read("tester");




            Assert.Equal(goalWeightModel.GoalWeight, ((GoalWeightModel)res.Result).GoalWeight);



            await _dao.Delete("tester");


        }




        [Theory]
        [MemberData(nameof(LoadGoalWeightJson))]
        public async void UpdateTest(GoalWeightModel goalWeightModel)
        {

            await _dao.Delete("tester");
            await _dao.Create(goalWeightModel, "tester");
            goalWeightModel.ExerciseLevel =  10;
            await _dao.Update(goalWeightModel, "tester");


            IWeightManagerResponse res = await _dao.Read("tester");





            Assert.Equal(goalWeightModel.ExerciseLevel, ((GoalWeightModel)res.Result).ExerciseLevel);




            await _dao.Delete("tester");


        }







        [Theory]
        [MemberData(nameof(LoadGoalWeightJson))]
        public async void Delete(GoalWeightModel goalWeightModel)
        {

            await _dao.Create(goalWeightModel, "tester");
            await _dao.Delete("tester");




            IWeightManagerResponse res = await _dao.Read("tester");


            Assert.NotEqual(goalWeightModel.GoalWeight, ((GoalWeightModel)res.Result).GoalWeight);




        }


        [Theory]
        [MemberData(nameof(LoadFoodJson))]

        public async void CreateFoodLogShould(FoodModel food)
        {
            await _dao.CreateFoodLog(food, "testuser");

            IWeightManagerResponse res = await _dao.GetFoodLogs("testuser");

            IEnumerable<FoodModel> foodLogs = (IEnumerable<FoodModel>)res.Result;


            Assert.Equal(foodLogs.First().Calories, food.Calories);



            await _dao.DeleteFoodLog((int)foodLogs.First().Id, "testuser");


        }

        [Theory]
        [MemberData(nameof(LoadFoodJson))]
        public async void GetFoodLogsAfterShould(FoodModel food)
        {
            await _dao.CreateFoodLog(food,"testuser");



            IWeightManagerResponse res = await _dao.GetFoodLogsAfter(new DateTime(1990, 01, 01), "testuser");
            FoodModel foodModel = ((IEnumerable<FoodModel>)res.Result).First();




            Assert.Equal(foodModel.Calories, food.Calories);



           await _dao.DeleteFoodLog((int)foodModel.Id, "testuser");


        }

        [Theory]
        [InlineData("dasdas","testuser")]
        [InlineData(@"C:/asdas", "testuser")]
        [InlineData(@"D://asdas", "testuser")]

        public async void SaveImagePathShould(string path, string username)
        {
             await _dao.SaveImagePath(path, DateTime.UtcNow, username);
             IWeightManagerResponse res = await _dao.GetAllImageIDs(username);

             IEnumerable<string> idEnum = ((IEnumerable<string>) res.Result);



             //SEE IF GOT ADDED
             Assert.False(idEnum.IsNullOrEmpty());



             await _dao.DeleteImagePath(Int32.Parse(idEnum.First()), username);

        }


        [Theory]
        [InlineData("dasdas", "testuser")]
        [InlineData(@"C:/asdas", "testuser")]
        [InlineData(@"D://asdas", "testuser")]
        public async void GetImageShould(string path, string username)
        {
            await _dao.SaveImagePath(path, DateTime.UtcNow, username);
            IWeightManagerResponse res = await _dao.GetAllImageIDs(username);

            IEnumerable<string> idEnum = ((IEnumerable<string>)res.Result);
            IWeightManagerResponse get = await _dao.GetImage(Int32.Parse(idEnum.First()), username);
            Assert.Equal(get.Result,path);


            await _dao.DeleteImagePath(Int32.Parse(idEnum.First()), username);

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
