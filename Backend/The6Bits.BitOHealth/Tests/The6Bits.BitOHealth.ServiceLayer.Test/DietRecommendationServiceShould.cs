using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;
using Xunit;

namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class DietRecommendationServiceShould : TestsBase
    {
        private DietRecommendationsService _serv;
        public DietRecommendationServiceShould()
        {
            _serv = new DietRecommendationsService(new DietRecommendationsMsSqlDao(conn), new MsSqlDerrorService());
        }
             public string _connectionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        [Fact]
        public void SaveTest()
        {
            DietR d = new DietR();
            d.Diet = "low-fat";
            d.Health = "peanut-free";
            d.Ingr = 5;
            d.DishType = "dessert";
            d.CuisineType = "American";
            d.MealType = "Lunch";
            d.Calories = 500;
            d.Excluded = "Almonds";
            d.Q = "fruit";
            _serv.SaveDietResponses(d);
                

               
        }



    }

    }
//}
    

