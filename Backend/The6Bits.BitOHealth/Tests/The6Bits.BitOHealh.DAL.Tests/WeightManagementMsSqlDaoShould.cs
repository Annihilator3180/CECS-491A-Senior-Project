using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class WeightManagementMsSqlDaoShould : TestsBase
    {

        private IRepositoryWeightManagementDao _dao;
        public WeightManagementMsSqlDaoShould()
        {
            _dao = new WeightManagementMsSqlDao(conn);
        }




        [Theory]
        [MemberData(nameof(LoadUsersJson))]
        public void CreateTest(GoalWeightModel goalWeightModel)
        {

            _dao.Delete("tester");


            _dao.Create(goalWeightModel, "tester");


            Assert.Equal(goalWeightModel.GoalWeight, _dao.Read("tester").GoalWeight);



            _dao.Delete("tester");


        }




        [Theory]
        [MemberData(nameof(LoadUsersJson))]
        public void UpdateTest(GoalWeightModel goalWeightModel)
        {

            _dao.Delete("tester");


            _dao.Create(goalWeightModel, "tester");
            goalWeightModel.ExerciseLevel =  10;
            _dao.Update(goalWeightModel,"tester");


            Assert.Equal(goalWeightModel.ExerciseLevel, _dao.Read("tester").ExerciseLevel);



            _dao.Delete("tester");


        }







        [Theory]
        [MemberData(nameof(LoadUsersJson))]
        public void Delete(GoalWeightModel goalWeightModel)
        {



            _dao.Create(goalWeightModel, "tester");

            _dao.Delete("tester");



            Assert.NotEqual(goalWeightModel.GoalWeight, _dao.Read("tester").GoalWeight);




        }








        public static IEnumerable<object[]> LoadUsersJson()
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



    }
}
