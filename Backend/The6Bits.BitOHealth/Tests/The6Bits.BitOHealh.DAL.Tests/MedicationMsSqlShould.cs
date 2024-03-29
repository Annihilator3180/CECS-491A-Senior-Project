﻿using System;
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
            string username = "test";
            medicationDAO.DeleteUsersList(username);
            DrugName testDrug = new DrugName("generic drug test", "test id", "brand name test");
            //act
            bool isAdded = medicationDAO.addFavorite(username, testDrug);
            int count = medicationDAO.getFavoriteCount(username);
            //assert
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

        }
        [Fact]
        public void updateFavoriteRemove()
        {

            //arrange 
            DrugName testDrug = new DrugName("generic drug test", "test id", "brand name test");
            FavoriteDrug testFavoriteDrug = new FavoriteDrug("generic drug test", "test id", "brand name test", 4, "test location","this is a test");
            string username = "test";
            medicationDAO.RemoveFavorite(testDrug.product_ndc, username);
            
            bool isAdded = medicationDAO.addFavorite(username, testDrug);
            //act
            medicationDAO.UpdateFavorite(username, testFavoriteDrug);
            FavoriteDrug readTest = medicationDAO.Read(username, testDrug.generic_name);
            //assert
            Assert.Equal(readTest.lowestprice,testFavoriteDrug.lowestprice);
            Assert.Equal(testFavoriteDrug.lowestPriceLocation, readTest.lowestPriceLocation);
            //cleanup
            medicationDAO.RemoveFavorite(testDrug.product_ndc, username);
        }
        [Fact]
        public void viewFavListShould()
        {
            //arrange 
            DrugName testDrug = new DrugName("generic drug test", "test id", "brand name test");
            DrugName testDrug2 = new DrugName("generic drug test2", "test id2", "brand name test2");
            string username = "test";
            medicationDAO.DeleteUsersList(username);
            medicationDAO.addFavorite(username, testDrug);
            medicationDAO.addFavorite(username, testDrug2);
            //act
            List<FavoriteDrug> favdrug= medicationDAO.ViewFavorites(username);
            //assert
            Assert.Equal(2,favdrug.Count);
            //cleanup 
            medicationDAO.DeleteUsersList(username);


        }
    }
}