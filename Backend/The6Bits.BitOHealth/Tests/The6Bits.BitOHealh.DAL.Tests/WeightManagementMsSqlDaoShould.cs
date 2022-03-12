using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Implementations;
using Xunit;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class WeightManagementMsSqlDaoShould : TestsBase
    {

        private IRepositoryWeightManagementDao _dao;
        public WeightManagementMsSqlDaoShould()
        {
            _dao = new WeightManagementMsSqlDao(conn);
        }



        [Theory]
        [InlineData(1, "bruh")]
        [InlineData(12222222, "bruh")]
        [InlineData(12222222, "zzzzzzzzzzzz")]
        public void CreateTest(int goalNum, string username)
        {

            _dao.Delete(username);


            _dao.Create(goalNum, username);


            Assert.Equal(goalNum, _dao.Read(username).Goal);



            _dao.Delete(username);


        }




        [Theory]
        [InlineData(12, "bruh")]
        [InlineData(12222222, "bruh")]
        [InlineData(12222222, "zzzzzzzzzzzz")]
        public void Delete(int goalNum, string username)
        {



            _dao.Create(goalNum, username);




            _dao.Delete(username);



            Assert.NotEqual(goalNum, _dao.Read(username).Goal);





        }


    }
}
