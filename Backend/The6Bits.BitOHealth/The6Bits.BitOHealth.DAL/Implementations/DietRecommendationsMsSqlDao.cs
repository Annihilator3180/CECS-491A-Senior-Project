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
        public string _connectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        // public DietRecommendationsMsSqlDao(string connectionString)
        //{
        //  _connectionString = connectionString;
        // }
        public string SaveDietResponses(DietR d)
        {
            try
            {
                string query = "INSERT Diet(Diet,Health, Ingr, DishType, Calories, CuisineType, Excluded, MealType) values (@Diet, @Health, @Ingr, @DishType,@Calories ,@CuisineType,@Excluded,@MealType) ";
                using( SqlConnection connection = new SqlConnection(_connectionString))
                {
                    int linesAdded = connection.Execute(query,
                        new
                        {
                            Diet = d.Diet,
                            Health = d.Health,
                            Ingr = d.Ingr,
                            DishType = d.DishType,
                            Calories = d.Calories,
                            CuisineType = d.CuisineType,
                            Excluded = d.Excluded,
                            MealType = d.MealType
                        }); ;
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
    }
}
