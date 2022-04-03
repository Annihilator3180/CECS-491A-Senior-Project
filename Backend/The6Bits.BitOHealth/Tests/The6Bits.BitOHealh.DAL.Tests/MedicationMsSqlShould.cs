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
        public void FavoriteCountShould()
        {

            //arrange 
            DrugName testDrug = new DrugName("generic drug test", "test id", "brand name test");
            string username = "test";
            medicationDAO.RemoveFavorite(username, "test id");
            //act
            bool isAdded = medicationDAO.addFavorite(username, testDrug);
            int count = medicationDAO.getFavoriteCount(username);
            //asset
            Assert.Equal(1, count);
            medicationDAO.RemoveFavorite( "test id", username);

        }
        [Fact]
        public void addFavoriteShould()
        {
            //arrange 
            DrugName testDrug = new DrugName("generic drug test", "test id", "brand name test");
            string username = "test";
            medicationDAO.RemoveFavorite(username, "test id");
            //act
            bool isAdded = medicationDAO.addFavorite(username, testDrug);
            //assert
            Assert.True(isAdded);
            //cleanup
            medicationDAO.RemoveFavorite("test id", username);

        }
        [Fact]
        public void removeFavoriteShould()
        {
            //arrange 
            DrugName testDrug = new DrugName("generic drug test", "test id", "brand name test");
            string username = "test";
            medicationDAO.RemoveFavorite("test id", username);
            bool isAdded = medicationDAO.addFavorite(username, testDrug);
            //act
            int isRemoved = medicationDAO.RemoveFavorite("test id", username);

            //assert
            Assert.True(isRemoved >0);
            //cleanup

        }
        [Fact]
        public void updateFavoriteRemove()
        {

            //arrange 
            DrugName testDrug = new DrugName("generic drug test", "test id", "brand name test");
            FavoriteDrug testFavoriteDrug = new FavoriteDrug("test", "generic drug test", "test id", "brand name test", 4, "test location");
            string username = "test";
            medicationDAO.RemoveFavorite(testDrug.product_id, username);
            
            bool isAdded = medicationDAO.addFavorite(username, testDrug);
            //act
            medicationDAO.UpdateFavorite(username, testFavoriteDrug);
            FavoriteDrug readTest = medicationDAO.Read(username, testDrug.product_id);
            //assert
            Assert.Equal(readTest.lowestprice,testFavoriteDrug.lowestprice);
            Assert.Equal(testFavoriteDrug.lowestPriceLocation, readTest.lowestPriceLocation);
            //cleanup
            medicationDAO.RemoveFavorite(testDrug.product_id, username);
        }
        [Fact]
        public void viewFavListShould()
        {
            DrugName testDrug = new DrugName("generic drug test", "test id", "brand name test");
            DrugName testDrug2 = new DrugName("generic drug test2", "test id2", "brand name test2");
            string username = "test";
            medicationDAO.RemoveFavorite(testDrug.product_id, username);
            medicationDAO.RemoveFavorite(testDrug2.product_id, username);
            medicationDAO.addFavorite(username, testDrug);
            medicationDAO.addFavorite(username, testDrug2);
            List<FavoriteDrug> favdrug= medicationDAO.ViewFavorites(username);
            Assert.Equal(2,favdrug.Count);

            
        }
    }
}