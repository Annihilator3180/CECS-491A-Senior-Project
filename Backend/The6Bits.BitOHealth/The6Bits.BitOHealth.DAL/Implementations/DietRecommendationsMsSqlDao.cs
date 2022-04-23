using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class DietRecommendationsMsSqlDao : IRepositoryDietRecommendations

    {
        private string _connectionString;
        public DietRecommendationsMsSqlDao(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string SaveDietResponses(DietR diet, string username)
        {
            try
            {
                string query = "INSERT INTO Diet(Username,Diet,Health, Ingr, DishType, Calories, CuisineType, Excluded, MealType) values ( @Username, @Diet, @Health, @Ingr, @DishType,@Calories ,@CuisineType,@Excluded,@MealType) ";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    int linesAdded =  connection.Execute(query,
                        new
                        {
                            Username = username,
                            Diet = diet.Diet,
                            Health = diet.Health,
                            Ingr = diet.Ingr,
                            DishType = diet.DishType,
                            Calories = diet.Calories,
                            CuisineType = diet.CuisineType,
                            Excluded = diet.Excluded,
                            MealType = diet.MealType
                        }); ;
                    connection.Close();
                    if (linesAdded == 0)
                    {
                        return linesAdded.ToString();
                    }
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string AddToFavorite(FavoriteRecipe recipe, string username)
        {
            try
            {
                int todayFavCount = 0;
                string favCheckQuery = "select COUNT(Recipe_id) from FavoriteRecipe where Username='" + username + "' AND DateAdded = CONVERT(date, GETDATE())";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(favCheckQuery, connection);
                    //convert object to string
                    todayFavCount = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    connection.Close();
                }

                if(todayFavCount <= 4)
                {
                    string query = "INSERT INTO FavoriteRecipe (Username, Recipe_id, DateAdded) values (@Username, @Recipe_id, @DateAdded)";
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        int linesAdded = connection.Execute(query,
                            new
                            {
                                Username = username,
                                Recipe_id = recipe.Recipe_id,
                                DateAdded = DateTime.Now.Date
                            // IngredientLines = recipe.IngredientLines,

                        });
                        connection.Close();
                        if (linesAdded == 0)
                        {
                            return "Failed to add meal to meal list";
                        }
                        return "Added meal to meal list" ;
                    }
                } else
                {
                    return "You reached today's limit. Come back tommorow!";
                }
                
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string DeleteFavorite(FavoriteRecipe favoriteRecipe)
        {
            try
            {
                string query = "DELETE FROM FavoriteRecipe WHERE Recipe_id = '" + favoriteRecipe.Recipe_id + "'";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    int linesDeleted = connection.Execute(query);
                    connection.Close();
                    if (linesDeleted == 0)
                    {
                        return "Failed to delete meal from meal list";
                    }
                    return "Deleted meal from meal list";
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public List<string> GetFavorites(string username)
        {
            List<string> favs = new List<string>();
            try
            {
                string query = "SELECT * FROM FavoriteRecipe WHERE Username = '" + username + "'";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    //multiple results (ExecuteReader)
                    SqlDataReader r =  sqlCommand.ExecuteReader();
                    // retrieve recipe ids of the user 
                    if (r.HasRows)
                    {
                        while (r.Read())
                        {
                            favs.Add((string)r["Recipe_id"]);
                        }
                        connection.Close();
                    }
                    else
                    {
                        favs.Add("Meal list is empty");
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
            return favs;
        }

        
    }

}

