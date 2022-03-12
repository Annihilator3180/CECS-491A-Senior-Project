using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.DBErrors;
using Xunit;

namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class DietRecommendationServiceShould : TestsBase
    {
        private DietRecommendationsService _serv;
        public DietRecommendationServiceShould()
        {
            _serv = new DietRecommendationsService(new DietRecommendationsMsSqlDao(), new MsSqlDerrorService());
        }





        [Theory]
        [InlineData("Dsad")]
        public void CreateServiceTest(string p)
        {
            _serv.SaveDietResponses(p);
        }


       // public void DeleteServiceTest(string p)
        //{
          //  _service.CreateGoal(1, p);
        }

    }
//}
    

