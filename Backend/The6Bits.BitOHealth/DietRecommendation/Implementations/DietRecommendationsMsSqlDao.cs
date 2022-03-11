using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DietRecommendation.Contract;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;

namespace DietRecommendation.Implementations
{
    public class DietRecommendationsMsSqlDao : IDietRecommendations
    {
        private string _connectionString;
        //public DietRecommendationsMsSqlDao(string connectionString)
        //{
        //  _connectionString = connectionString;
        // }
        public string SaveDietResponses(DietR d)
        {
            try
            {
                string query = "INSERT USER_DIET (Diet,Health, Ingr, DishType, Calories, Time, Excluded) " +
                               " values (@Diet, @Health, @Ingr, @DishType,@Calories @Time,@Excluded) ";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    int linesAdded = connection.Execute(query,
                        new
                        {
                            Diet = d.Diet,
                            Health = d.Health,
                            Ingr = d.Ingr,
                            DishType = d.DishType,
                            Calories = d.Calories,
                            Time = d.Time,
                            Excluded = d.Excluded
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
    }
}
