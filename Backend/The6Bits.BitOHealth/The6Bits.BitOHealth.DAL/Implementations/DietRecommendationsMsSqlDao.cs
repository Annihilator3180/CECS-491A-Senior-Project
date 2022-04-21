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
                using( SqlConnection connection = new SqlConnection(_connectionString))
                {
                    int linesAdded = connection.Execute(query,
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
                        });;
                    connection.Close();
                    if(linesAdded==0)
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

        public async Task<String> AddToFavorite(FavoriteRecipe recipe, string username)
        {
            try
            {
                string query = "INSERT INTO FavoriteRecipe (Username, Recipe_id) values (@Username, @Recipe_id)";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    int linesAdded = connection.Execute(query,
                        new
                        {
                            Username = username,
                            Recipe_id = recipe.Recipe_id
                            // IngredientLines = recipe.IngredientLines,

                        });
                    connection.Close();
                    if (linesAdded == 0)
                    {
                        return linesAdded.ToString();
                    }
                    return "1";
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public async Task<string> DeleteFavorite(string recipeid)
        {
            try
            {
                string query = "DELETE FROM FavoriteRecipe WHERE Recipe_id = '" + recipeid + "'";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    int linesDeleted = connection.Execute(query);
                    connection.Close();
                    if (linesDeleted == 0)
                    {
                        return linesDeleted.ToString();
                    }
                    return "1";
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public async Task<List<string>> GetFavorites(string username)
        {
            List<string> favs = new List<string>();
            try
            {
                string query = "SELECT * FROM FavoriteRecipe WHERE Username = '"+username+"'";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    // retrieve recipe ids of the user 
                    while (reader.Read())
                    {
                        favs.Add((string)reader["Recipe_id"]);
                    }
                    connection.Close();
                    
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
