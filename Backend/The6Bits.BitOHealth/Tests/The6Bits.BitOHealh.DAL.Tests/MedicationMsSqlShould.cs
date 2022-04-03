using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Tests
{
    public class MedicationMsSqlShould : TestsBase
    {
        private IRepositoryMedication<string> medicationDAO;

        public MedicationMsSqlShould()
        {
            medicationDAO = new MsSqlMedicationDAO(conn);
        }
        [Fact]
        public void addFavoriteShould()
        {
            //arrange 
            DrugName testDrug=new DrugName("generic drug test","test id", "brand name test");
            string username = "test";
            //act
            int isAdded = medicationDAO.getFavoriteCount(username);
            //asset
            Assert.Equal(5, isAdded);

        }
    }
}
