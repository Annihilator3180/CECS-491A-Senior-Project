using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class NutritionAnalysisMsSqlDao : IRepositoryNutritionAnalysis

    {
        private string _connectionString;
        public NutritionAnalysisMsSqlDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string SaveRecipeResponse(string username, Ingredients ingredient)
        {
            try
            {
                string query = "INSERT INTO NutritionAnalysis(Username, ingr) values (@Username, @ingr) ";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    int linesAdded = connection.Execute(query,
                        new
                        {
                            Username = username,
                            ingr = ingredient.ingr,

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
