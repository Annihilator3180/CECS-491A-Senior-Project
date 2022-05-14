using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class BMICalculatorMsSqlDao : IRepositoryBMICalculator
    {

        private string _connectionString;
        public BMICalculatorMsSqlDao(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string SaveBMI(string username, double height, double weight, string bmi)
        {
            try
            {
                string query = "INSERT INTO BMI(username ,height ,weight, bmi) values ( @username, @height, @wieght, @bmi) ";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    int linesAdded = connection.Execute(query,
                        new
                        {
                            username = username,
                            height = height,
                            weight = weight,
                            bmi = bmi
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
