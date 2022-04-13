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
             
                string query = $"select Count(@username) from favoriteMedication where Username=@username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int favCount = connection.ExecuteScalar<int>(query,
                        new
                        {
                            username = username,
                        }); 
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
                string query = "INSERT favoriteMedication(Username, product_id , generic_name ," +
                    " brand_name, lowestPrice , lowestPriceLocation)values(@Username, @product_id, " +
                    "@generic_name, @brand_name,0, @lowestPriceLocation)";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    connection.Execute(query,
                        new
                        {
                            Username = username,
                            product_id = drug.product_id,
                            generic_name = drug.generic_name,
                            brand_name = drug.brand_name,
                            lowestPriceLocation = ""
                        }); 
                    connection.Close();
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public List<FavoriteDrug> ViewFavorites(string username)
        {
            try
            {
                string query = "select * from favoriteMedication where username= @username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<FavoriteDrug> str = connection.Query<FavoriteDrug>(query, new { username = username });
                    return str.ToList();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public int RemoveFavorite(string drugProductID, string username)
        {
            try
            {

                string query = "delete from favoriteMedication where username=@username and product_id=@product_id";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int deleted = connection.Execute(query,
                        new
                        {
                            username = username,
                            product_id = drugProductID,


                        }); 
                    connection.Close();
                    return deleted;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
        public int UpdateFavorite(string username, FavoriteDrug drug)
        {
            try
            {

                string query = "update favoriteMedication " +
                    "set username = @username, " +
                    "lowestPrice = @lowestPrice, " +
                    "lowestPriceLocation = @lowestPriceLocation " +
                    "where username = @username and product_id = @product_id";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int favCount = connection.Execute(query,
                        new
                        {
                            username = username,
                            product_id = drug.product_id,
                            lowestPrice=drug.lowestprice,
                            lowestPriceLocation = drug.lowestPriceLocation
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
        public FavoriteDrug Read(string username, string drugName)
        {
            try
            {
                string query = $"Select * from favoriteMedication " +
                       "where username = @username and generic_name = @generic_name";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<FavoriteDrug> favDrug = connection.Query<FavoriteDrug>(query,
                        new
                        {
                            username = username,
                            generic_name = drugName
                        });
                    connection.Close();
                    try
                    {
                        return favDrug.First();
                    }
                    catch(Exception ex)
                    {
                        return new FavoriteDrug();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number.ToString());
            }
        }
}


    }

