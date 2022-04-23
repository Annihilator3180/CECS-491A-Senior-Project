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

        [Fact]
        public void SaveTest()
        {
            string username = "emily";
            DietR d = new DietR("fruit","low-fat","peanut-free",5,"dessert",500,"American","Almonds","Lunch");
            _serv.SaveDietResponses(d,username);   
        }




    }

    }

    

