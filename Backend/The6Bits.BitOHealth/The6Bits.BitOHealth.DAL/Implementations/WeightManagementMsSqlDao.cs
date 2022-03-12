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

        public string Create(int goalNum, string username)
        {
            try
            {
                string query = $"INSERT INTO WMGoals (Username, Goal) values (@Username, @Goal)";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int count = connection.Execute(query, new
                    {
                        Username = username,
                        Goal = goalNum
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
                    IEnumerable<GoalWeightModel> mod = connection.Query<GoalWeightModel>("select * from WMGoals where Username = @Username", new { Username = username });
                    return mod.First();
                }
            }
            catch (Exception ex)
            {
                

                return new GoalWeightModel("100", 1);
            }
        }


    }
}
