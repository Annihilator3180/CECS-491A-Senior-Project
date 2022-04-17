using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using Xunit;

namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class WeightManagementServiceShould : TestsBase
    {

        private readonly WeightManagementService _service;
        public WeightManagementServiceShould()
        {
            _service = new WeightManagementService(new WeightManagementMsSqlDao(conn));
        }

        /**
        [Theory]
        [InlineData("Dsad")]
        public void CreateServiceTest(string p)
        {
            _service.CreateGoal(1, p);
        }


        public void DeleteServiceTest(string p)
        {
            _service.CreateGoal(1, p);
        }**/

    }
}
