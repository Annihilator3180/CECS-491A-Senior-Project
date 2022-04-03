﻿using System;
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
             
                string query = "select count(*) from favoriteMedication where username=@username";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int favCount = connection.Execute(query,
                        new
                        {
                            username = username,
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
                string query = "INSERT favoriteMedication(Username, MedicineProductID, MedicineGenericName, MedicineBrandName, lowestPrice, lowestPricefound)values(@Username, @MedicineProductID, @MedicineGenericName @MedicineBrandName,0, \"\")";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
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

                string query = "delete * from favoriteMedication where username=@username and MedicineProductID=@DrugProductID";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int favCount = connection.Execute(query,
                        new
                        {
                            username = username,
                            drugProductID = drugProductID,


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
        public int UpdateFavorite(string username, FavoriteDrug drug)
        {
            try
            {

                string query = "update favoriteMedication " +
                    "set(username = @username, lowestPrice = @lowestPrice, lowestFoundPrice = @lowestFoundPrice) " +
                    "where username = @username and DrugProductID = @DrugProductID";
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    int favCount = connection.Execute(query,
                        new
                        {
                            username = username,
                            drugProductID = drug.product_id,
                            lowestPrice=drug.lowestprice,
                            lowestFoundPrice=drug.lowestPriceLocation
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


    }
}
