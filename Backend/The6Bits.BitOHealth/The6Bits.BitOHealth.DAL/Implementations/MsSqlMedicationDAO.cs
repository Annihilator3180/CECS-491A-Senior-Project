using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL
{
    public class MsSqlMedicationDAO : IRepositoryMedication<string>
    {
        private string _connectString;
        public MsSqlMedicationDAO(string connectstring)
        {
            _connectString = connectstring;


        }
        public int getFavoriteCount(string username)
        {
            try
            {
                string query = "INSERT ACCOUNTS (Username,Email,Password,FirstName,LastName,IsEnabled,IsAdmin,privOption) " +
                               " values (@Username, @Email, @Password, @FirstName,@LastName, @IsEnabled,@IsAdmin,@privOption) ";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    int favCount = connection.Execute(query,
                        new
                        {
                            Username = username,
                        }); ;
                    connection.Close();
                    return favCount;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }

        }

        public bool addFavorite(string username, DrugName drug)
        {
            try
            {
                string query = "INSERT INTO favoriteMedication(username, MedicineProductID, MedicineGenericName, MedicineBrandName, lowestPrice, lowestFoundPrice)" +
                    "values(@Username, @MedicineProductID, @MedicineGenericName @MedicineBrandName,0, 0)";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Execute(query,
                        new
                        {
                            Username = username,
                            MedicineProductID = drug.product_id,
                            MedicineGenericName = drug.generic_name,
                            MedicineBrandName = drug.brand_name,
                        }); ;
                    connection.Close();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }

    }
}
