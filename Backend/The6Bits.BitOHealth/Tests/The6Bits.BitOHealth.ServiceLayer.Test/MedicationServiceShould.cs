using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;
using The6Bits.BitOHealth.Models;
using Xunit;
using System.Net.Http;
namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class MedicationServiceShould : TestsBase
    {
        private MedicationService _MS;
        private IRepositoryMedication<string> _dao;
        public MedicationServiceShould()
        {
            _dao = new MsSqlMedicationDAO(conn);
            //_MS = new MedicationService();
            _MS = new MedicationService(_dao, new OpenFDADAO(new HttpClient(), _openFDA));
        }
        [Fact]
        public void ViewEmptyFavoritesTest()
        {
            //arrange
            _MS.DeleteFavoriteList("TestName");
            
            //act
            
            try
            {
                _MS.ViewFavorites("TestName");
            }
            catch (Exception ex)
            {
                //assert
                Assert.Equal("no drugs found",ex.Message);
            }
            Assert.True(true);
        }
        [Fact]
        public void FavoritesListTest()
        {
            //arrabt
            _MS.DeleteFavoriteList("TestName");
            DrugName testName = new DrugName("test","test","test");
            _MS.addFavorite("TestName", testName);
            //act
            List<FavoriteDrug> favDrug = _MS.ViewFavorites("TestName");
            //assert
            Assert.True(favDrug.Count() == 1);
            //delete
            _MS.DeleteFavoriteList("TestName");
        }
        [Fact]
        public void FavoriteAddTest()
        {
            //arrange
            _MS.DeleteFavoriteList("TestName");
            DrugName testName = new DrugName("test", "test", "test");
            //act
            bool isAdded =_MS.addFavorite("TestName", testName);
            List<FavoriteDrug> favDrug = _MS.ViewFavorites("TestName");
            //assert
            Assert.True(favDrug.Count() == 1);
            Assert.True(isAdded);
            //delete
            _MS.DeleteFavoriteList("TestName");
        }
        [Fact]
        public void RemoveMedicationTest()
        {
            //arrange
            _MS.DeleteFavoriteList("TestName");
            DrugName testName = new DrugName("test", "test", "test");
            
            _MS.addFavorite("TestName", testName);
            //act
            string removed=_MS.RemoveFavorite("test", "TestName");
            try
            {
                List<FavoriteDrug> favDrug = _MS.ViewFavorites("TestName");
            }
            //assert
            catch (Exception ex)
            {
                Assert.True(ex.Message == "no drugs found");
            }
            
            
            Assert.Equal("Deleted Favorite", removed);
            //delete
            _MS.DeleteFavoriteList("TestName");
        }
        [Fact]
        public void NoRemoveMedicationTest()
        {
            //arrange
            _MS.DeleteFavoriteList("TestName");
            DrugName testName = new DrugName("test", "test", "test");

            //act
            string removed = _MS.RemoveFavorite("test", "TestName");
            try
            {
                List<FavoriteDrug> favDrug = _MS.ViewFavorites("TestName");
            }
            //assert
            catch (Exception ex)
            {

                Assert.True(ex.Message == "no drugs found");
            }
            
            Assert.Equal("no matches found", removed);
            //delete
            _MS.DeleteFavoriteList("TestName");
        }
        [Fact]
        public void validPriceTest()
        {
            //arrange
            int price = 20;
            //act
            bool valid=_MS.ValidatePrice(price);
            //arrange
            Assert.True(valid);

        }
        [Fact]
        public void negativePriceTest()
        {
            //arrange
            int price = -20;
            //act
            bool valid = _MS.ValidatePrice(price);
            //arrange
            Assert.False(valid);

        }
        [Fact]
        public void expensivePriceTest()
        {
            //arrange
            int price = 90000001;
            //act
            bool valid = _MS.ValidatePrice(price);
            //arrange
            Assert.False(valid);

        }
        [Fact]
        public void validLocation()
        {
            //arrange
            string location = "cvs";
            //act
            bool valid = _MS.ValidateLocation(location);
            //arrange
            Assert.True(valid);

        }
        [Fact]
        public void validDescription()
        {
            //arrange
            string description = "this is a sample";
            //act
            bool isValid=_MS.validateDescription(description);
            //assert
            Assert.True(isValid);
        }
        [Fact]

        public void invalidLocation()
        {
            //arrange
            string location = "cvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvs" +
                "cvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvscvs";
            //act
            bool valid = _MS.ValidateLocation(location);
            //arrange
            Assert.False(valid);

        }
        [Fact]
        public void DescriptionValid()
        {
            //arrange
            string description = "cvs/100";
            //act
            string descriptionResult = _MS.CreateDescription(description);
            //assert
            Assert.Equal("Cheapest reported price is 100 Found at cvs", descriptionResult);
        }
        [Fact]
        public void TitleValid()
        {
            //arrange
            string description = "coffee";
            //act
            string descriptionResult = _MS.CreateTitle(description);
            //assert
            Assert.Equal("Reminder: coffee refill", descriptionResult);
        }

        [Fact]
        public void CheckDuplicatesTest()
        {
            //arrange
            DrugName drug1 = new DrugName("1", "1", "1");
            DrugName drug2 = new DrugName("2", "2", "2");
            DrugName drug3 = new DrugName("3", "3", "3");

            List<DrugName> testDrugs = new List<DrugName> { drug1, drug2 };
            List<DrugName> testDuplicates = new List<DrugName> { drug1, drug3 };
            //act
            List<DrugName> combinedDrugs = _MS.CheckDuplicates(testDrugs, testDuplicates);
            //arrange
            Assert.Equal(3, combinedDrugs.Count);
        }


    }
}
