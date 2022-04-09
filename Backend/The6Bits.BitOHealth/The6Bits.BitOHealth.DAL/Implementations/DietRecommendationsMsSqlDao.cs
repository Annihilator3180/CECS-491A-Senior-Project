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
        public string SaveDietResponses(DietR d)
        {
            try
            {
                string query = "INSERT Diet(Q,Diet,Health, Ingr, DishType, Calories, CuisineType, Excluded, MealType, From, To) values (@Q,@Diet, @Health, @Ingr, @DishType,@Calories ,@CuisineType,@Excluded,@MealType, @From, @To) ";
                using( SqlConnection connection = new SqlConnection(_connectionString))
                {
                    int linesAdded = connection.Execute(query,
                        new
                        {
                            Q=d.Q,
                            Diet = d.Diet,
                            Health = d.Health,
                            Ingr = d.Ingr,
                            DishType = d.DishType,
                            Calories = d.Calories,
                            CuisineType = d.CuisineType,
                            Excluded = d.Excluded,
                            MealType = d.MealType,
                            From = d.From,
                            To = d.To,
                        }); ; ;
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
