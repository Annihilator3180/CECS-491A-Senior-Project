using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class WeightManagementMsSqlDao : IRepositoryWeightManagementDao
    { 
        private string _connectString;



        public WeightManagementMsSqlDao(string connectString)
        {
            _connectString = connectString;
        }

        public string Delete(string username)
        {
            try
            {
                string query = $"Delete From WMGoals where Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.Execute(query, new { Username = username });
                    if (count != 0)
                    {
                        return "deleted Weight Goal";
                    }
                    return "no Weight Goal";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }

        public string Create(GoalWeightModel goal, string username)
        {
            try
            {
                string query = $"INSERT INTO WMGoals (Username, GoalWeight, GoalDate, ExerciseLevel ) values (@Username, @GoalWeight, @GoalDate, @ExerciseLevel)";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.Execute(query, new
                    {
                        Username = username,
                        GoalWeight = goal.GoalWeight,
                        GoalDate = goal.GoalDate,
                        ExerciseLevel = goal.ExerciseLevel
                    });
                    if (count != 0)
                    {
                        return "saved Weight Goal";
                    }

                    return "Weight Goal not saved";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
        }


        public GoalWeightModel Read(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<GoalWeightModel> responseEnumerable = connection.Query<GoalWeightModel>("select * from WMGoals where Username = @Username", new { Username = username });
                    if (responseEnumerable == null || !responseEnumerable.Any())
                    {
                        return new GoalWeightModel();
                    }

                    return responseEnumerable.First();
                }
            }
            catch (Exception ex)
            {
                //LOG

                return new GoalWeightModel( 1, DateTime.Now, 0);
            }
        }

        public string Update(GoalWeightModel goal, string username)
        {
            string query = "UPDATE WMGoals SET GoalWeight = @GoalWeight, GoalDate = @GoalDate, ExerciseLevel = @ExerciseLevel where Username = @Username";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.Execute(query, 
                    new
                    {
                        GoalWeight = goal.GoalWeight,
                        GoalDate = goal.GoalDate,
                        ExerciseLevel = goal.ExerciseLevel,
                        Username = username
                    });
                    if (count != 0)
                    {
                        return "udpated Weight Goal";
                    }

                    return "Weight Goal not updated";
                }
            }
            catch (SqlException ex)
            {
                return ex.Number.ToString();
            }
            
            
        }

        public string CreateFoodLog(FoodModel food, string username)
        {
            try
            {
                string query = $"INSERT INTO FoodLog (Username, FoodName, Description, Calories, FoodLogDate ,Carbs, Protein, Fat ) values (@Username, @FoodName, @Description, @Calories, @FoodLogDate, @Carbs, @Protein, @Fat )";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.Execute(query, new
                    {
                        Username = username,
                        FoodName = food.FoodName,
                        Description = food.Description,
                        Calories = food.Calories,
                        FoodLogDate = food.FoodLogDate,
                        Carbs = food.Carbs,
                        Protein = food.Protein,
                        Fat = food.Fat


                    }); ;
                    if (count != 0)
                    {
                        return "saved foodlog";
                    }

                    return "food log not saved";
                }
            }
            catch (SqlException ex)
            {
                //LOGG
                throw ex;
            }
        }

        public IEnumerable<FoodModel> GetFoodLogs(string username)
        {
            try
            {
                string query = $"Select FoodName, Description, Calories, FoodLogDate ,Carbs, Protein, Fat  FROM FoodLog WHERE Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<FoodModel> foodModels = connection.Query<FoodModel>(query, new
                    {
                        Username = username,

                    }); ;
                    if (foodModels != null || !foodModels.Any())
                    {
                        return foodModels;
                    }

                    return new List<FoodModel>();
                }
            }
            catch (SqlException ex)
            {
                //LOGG
                throw ex;
            }
        }



    }
}
