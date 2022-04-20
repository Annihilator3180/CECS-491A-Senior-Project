using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.Models.WeightManagement;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class WeightManagementMsSqlDao : IRepositoryWeightManagementDao<IWeightManagerResponse>
    { 
        private readonly string _connectString;



        public WeightManagementMsSqlDao(string connectString)
        {
            _connectString = connectString;
        }

        public async Task<IWeightManagerResponse> Delete(string username)
        {
            try
            {
                string query = $"Delete From WMGoals where Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = await connection.ExecuteAsync(query, new { Username = username });
                    if (count != 0)
                    {
                         return new WeightManagerResponse("deleted Weight Goal");
                    }
                    return new WeightManagerResponse("no Weight Goal");
                }
            }
            catch (SqlException ex)
            {
                 return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }

        public async Task<IWeightManagerResponse> Create(GoalWeightModel goal, string username)
        {
            try
            {
                string query = $"INSERT INTO WMGoals (Username, GoalWeight, GoalDate, ExerciseLevel, CurrentWeight ) values (@Username, @GoalWeight, @GoalDate, @ExerciseLevel, @CurrentWeight)";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = await connection.ExecuteAsync(query, new
                    {
                        Username = username,
                        GoalWeight = goal.GoalWeight,
                        GoalDate = goal.GoalDate,
                        ExerciseLevel = goal.ExerciseLevel,
                        CurrentWeight = goal.CurrentWeight,
                    });
                    if (count != 0)
                    {
                         return new WeightManagerResponse("saved Weight Goal");
                    }

                    return new WeightManagerResponse("Weight Goal not saved");
                }
            }
            catch (SqlException ex)
            {
                 return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }


        public async Task<IWeightManagerResponse> Read(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<GoalWeightModel> responseEnumerable = await connection.QueryAsync<GoalWeightModel>("select * from WMGoals where Username = @Username", new { Username = username });
                    if (responseEnumerable == null || !responseEnumerable.Any())
                    {
                         return new WeightManagerResponse(new GoalWeightModel());
                    }

                    return new WeightManagerResponse(responseEnumerable.First());
                }
            }
            catch (SqlException ex)
            {
                //LOG

                 return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }

        public async Task<IWeightManagerResponse> Update(GoalWeightModel goal, string username)
        {
            string query = "UPDATE WMGoals SET GoalWeight = @GoalWeight, GoalDate = @GoalDate, ExerciseLevel = @ExerciseLevel, CurrentWeight = @CurrentWeight where Username = @Username";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = await connection.ExecuteAsync(query, 
                        new
                        {
                            GoalWeight = goal.GoalWeight,
                            GoalDate = goal.GoalDate,
                            ExerciseLevel = goal.ExerciseLevel,
                            CurrentWeight = goal.CurrentWeight,
                            Username = username
                        });
                    if (count != 0)
                    {
                         return new WeightManagerResponse("updated Weight Goal");
                    }

                    return new WeightManagerResponse("Weight Goal not updated");
                }
            }
            catch (SqlException ex)
            {
                 return new WeightManagerResponse(ex.Number.ToString(), true);
            }
            
            
        }

        public async Task<IWeightManagerResponse> CreateFoodLog(FoodModel food, string username)
        {
            try
            {
                string query = $"INSERT INTO FoodLog (Username, FoodName, Description, Calories, FoodLogDate ,Carbs, Protein, Fat , DateAdded) values (@Username, @FoodName, @Description, @Calories, @FoodLogDate, @Carbs, @Protein, @Fat, @DateAdded )";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = await connection.ExecuteAsync(query, new
                    {
                        Username = username,
                        FoodName = food.FoodName,
                        Description = food.Description,
                        Calories = food.Calories,
                        FoodLogDate = food.FoodLogDate,
                        Carbs = food.Carbs,
                        Protein = food.Protein,
                        Fat = food.Fat,
                        DateAdded = DateTime.UtcNow


                    }); 
                    if (count != 0)
                    {
                         return new WeightManagerResponse("saved foodlog");
                    }

                    return new WeightManagerResponse("food log not saved");
                }
            }
            catch (SqlException ex)
            {
                //LOGG
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }

        public async Task<IWeightManagerResponse> GetFoodLogs(string username)
        {
            try
            {
                string query = $"Select FoodName, Description, Calories, FoodLogDate ,Carbs, Protein, Fat, Id  FROM FoodLog WHERE Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<FoodModel> foodModels = await connection.QueryAsync<FoodModel>(query, new
                    {
                        Username = username,

                    }); ;
                    if (foodModels != null || !foodModels.Any())
                    {
                         return new WeightManagerResponse(foodModels);
                    }

                    return new WeightManagerResponse(new List<FoodModel>());
                }
            }
            catch (SqlException ex)
            {
                //LOGG
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }


        public async Task<IWeightManagerResponse> GetFoodLogsAfter(DateTime dateTime, string username)
        {
            try
            {
                string query = $"Select FoodName, Description, Calories, FoodLogDate ,Carbs, Protein, Fat ,Id  FROM FoodLog WHERE Username = @Username AND FoodLogDate > @Date";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<FoodModel> foodModels = await connection.QueryAsync<FoodModel>(query, new
                    {
                        Username = username,
                        Date = dateTime,

                    }); ;
                    if (foodModels != null || !foodModels.Any())
                    {
                         return new WeightManagerResponse(foodModels);
                    }

                    return new WeightManagerResponse(new List<FoodModel>());
                }
            }
            catch (SqlException ex)
            {
                //LOGG
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }




        public async Task<IWeightManagerResponse> DeleteFoodLog(int id, string username)
        {
            try
            {
                string query = $"Delete FROM FoodLog Where Id = @id AND Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = await connection.ExecuteAsync(query, new
                    {
                        Username = username,
                        Id = id,

                    }); ;
                    if (count != 0)
                    {
                         return new WeightManagerResponse("deleted food log");
                    }

                    return new WeightManagerResponse("food log not deleted ");
                }
            }
            catch (SqlException ex)
            {
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }


        public async Task<IWeightManagerResponse> SaveImagePath(string path, DateTime imageDateTime, string username)
        {
            try
            {
                string query = $"INSERT INTO WeightGoalImages (Username, Path, ImageDate) values (@Username, @Path, @ImageDate)";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = await connection.ExecuteAsync(query, new
                    {
                        Username = username,
                        Path = path,
                        ImageDate = imageDateTime,
                    });
                    if (count != 0)
                    {
                        return new WeightManagerResponse("save image success");
                    }

                    return new WeightManagerResponse("save image fail");
                }
            }
            catch (SqlException ex)
            {
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }



        public async Task<IWeightManagerResponse> GetImage(int id, string username)
        {
            try
            {
                string query = $"Select Path FROM WeightGoalImages WHERE Username = @Username and Id = @Id";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<string> imagePaths = await connection.QueryAsync<string>(query, new
                    {
                        Username = username,
                        Id = id,
                    });
                    if (imagePaths != null || !imagePaths.Any())
                    {
                        return new WeightManagerResponse(imagePaths.First());
                    }

                    return new WeightManagerResponse("");
                }
            }
            catch (SqlException ex)
            {
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }



        public async Task<IWeightManagerResponse> GetAllImageIDs(string username)
        {
            try
            {
                string query = $"Select Id FROM WeightGoalImages WHERE Username = @Username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<string> ids = await connection.QueryAsync<string>(query, new
                    {
                        Username = username,
                    });
                    if (ids != null || !ids.Any())
                    {
                        return new WeightManagerResponse(ids);
                    }

                    return new WeightManagerResponse(new List<string>());
                }
            }
            catch (SqlException ex)
            {
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }


        public async Task<IWeightManagerResponse> GetFoodLogsAfterAddTime(DateTime dateTime ,string username)
        {
            try
            {
                string query = $"Select FoodName, Description, Calories, FoodLogDate ,Carbs, Protein, Fat ,Id  FROM FoodLog WHERE Username = @Username AND DateAdded > @Date";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<FoodModel> foodModels = await connection.QueryAsync<FoodModel>(query, new
                    {
                        Username = username,
                        Date = dateTime,

                    }); ;
                    if (foodModels != null || !foodModels.Any())
                    {
                        return new WeightManagerResponse(foodModels);
                    }

                    return new WeightManagerResponse(new List<FoodModel>());
                }
            }
            catch (SqlException ex)
            {
                //LOGG
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }


        public async Task<IWeightManagerResponse> DeleteImagePath(int id, string username)
        {
            try
            {
                string query = $"Delete From WeightGoalImages where Username = @Username AND Id = @id";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = await connection.ExecuteAsync(query, new { Username = username, Id = id });
                    if (count != 0)
                    {
                        return new WeightManagerResponse("deleted Weight Goal");
                    }
                    return new WeightManagerResponse("no Weight Goal");
                }
            }
            catch (SqlException ex)
            {
                return new WeightManagerResponse(ex.Number.ToString(), true);
            }
        }





    }
}
