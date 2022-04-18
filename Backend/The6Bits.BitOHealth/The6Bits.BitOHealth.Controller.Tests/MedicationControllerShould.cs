using Xunit;
using The6Bits.Authorization;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.ControllerLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.Logging.DAL.Contracts;
using System.Net.Http;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.DBErrors;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging.DAL.Implementations;
using System.Collections.Generic;

namespace The6Bits.Authorization.Tests
{
    public class MedicationControllerShould : ControllerBase
    {
        public List<String> testingInfo()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Path.Combine(AppContext.BaseDirectory, @"..\..\..\"))
            .AddJsonFile("appsettings.json")
            .AddJsonFile("secrets.json")
            .Build();
            string conn = configuration.GetConnectionString("DefaultConnection");
            string keyPath = configuration.GetSection("PK")["jwt"];
            string TestingToken = configuration.GetSection("PK")["TestingToken"];
            List<string> values=new List<string>();
            values.Add(conn);
            values.Add(keyPath);
            values.Add(TestingToken);  
            return values;
        }
        public MedicationController medicationContext()
        {
            List<string> vals = testingInfo();
            JWTAuthenticationService _authenticationService = new JWTAuthenticationService(vals[1]);
            MsSqlMedicationDAO _Dao = new MsSqlMedicationDAO(vals[0]);
            MedicationController controller = new MedicationController(_Dao, new OpenFDADAO(new HttpClient(), new openFDAConfig()), new SQLLogDAO(),
            _authenticationService, new MsSqlDerrorService(), new ReminderMsSqlDao(vals[0]));

            DefaultHttpContext context = new DefaultHttpContext();
            context.Request.Headers.Add("Authorization", vals[2]);
            controller.ControllerContext.HttpContext = context;
            return controller;
        }
        public MedicationController badContext()
        {
            List<string> vals = testingInfo();
            JWTAuthenticationService _authenticationService = new JWTAuthenticationService(vals[1]);
            MsSqlMedicationDAO _Dao = new MsSqlMedicationDAO(vals[0]);
            MedicationController controller = new MedicationController(_Dao, new OpenFDADAO(new HttpClient(), new openFDAConfig()), new SQLLogDAO(),
            _authenticationService, new MsSqlDerrorService(), new ReminderMsSqlDao(vals[0]));

            DefaultHttpContext context = new DefaultHttpContext();
            context.Request.Headers.Add("Authorization", "");
            controller.ControllerContext.HttpContext = context;
            return controller;
        }

        [Fact]
        public void addFavTest()
        {

            //arrange
            MedicationController medicationController = medicationContext();
            string testNDC = "123";
            string TestGeneric = "generic";
            string TestBrand = "brand";
            medicationController.RemoveFavorite(testNDC);
            //act
            string s = medicationController.AddFavorites(TestBrand, TestGeneric, testNDC);
            //assert
            Assert.Equal("Favorited", s);
            //cleanup
            medicationController.RemoveFavorite(testNDC);

        }
        [Fact]
        public void addTestTokenFailTest()
        {

            //arrange
            MedicationController medicationController = badContext();
            string testNDC = "123";
            string TestGeneric = "generic";
            string TestBrand = "brand";
            //act
            medicationController.RemoveFavorite(testNDC);
            string s = medicationController.AddFavorites(TestBrand, TestGeneric, testNDC);
            medicationController.RemoveFavorite(testNDC);
            //arrange
            Assert.Equal("invalid token", s);


        }
        [Fact]
        public void addTestDupTest()
        {

            //arrange
            MedicationController medicationController = medicationContext();
            string testNDC = "123";
            string TestGeneric = "generic";
            string TestBrand = "brand";
            medicationController.RemoveFavorite(testNDC);
            //act
            medicationController.AddFavorites(TestBrand, TestGeneric, testNDC);
            string s = medicationController.AddFavorites(TestBrand, TestGeneric, testNDC);
            //assert
            Assert.Equal("already favorited", s);
            //cleanup
            medicationController.RemoveFavorite(testNDC);

        }
        [Fact]
        public void removeTest()
        {

            //arrange
            MedicationController medicationController = medicationContext();
            string testNDC = "123";
            string TestGeneric = "generic";
            string TestBrand = "brand";
            //act
            medicationController.RemoveFavorite(testNDC);
            medicationController.AddFavorites(TestBrand, TestGeneric, testNDC);
            string s= medicationController.RemoveFavorite(testNDC);
            //assert
            Assert.Equal("Deleted Favorite", s);


        }
        [Fact]
        public void removeTestInvalidToken()
        {

            //arrange
            MedicationController medicationController = badContext();
            string testNDC = "123";
            //act
            string s = medicationController.RemoveFavorite(testNDC);
            //assert
            Assert.Equal("invalid token", s);
        }
        [Fact]
        public void removeTestFail()
        {
            //arrange
            MedicationController medicationController = medicationContext();
            string testNDC = "123";
            //act
            medicationController.RemoveFavorite(testNDC);
            string s = medicationController.RemoveFavorite(testNDC);
            //assert
            Assert.Equal("no matches found", s);
           
        }
        [Fact]
        public void FindDrugTestInvalid()
        {
            //arrange
            MedicationController medicationController = medicationContext();
            string testName = "bakjasldklad";
            //act
            FindDrugResponse drug = medicationController.FindDrug(testName);
            //assert
            Assert.False(drug.success);
        }
        [Fact]
        public void FindDrugTestInvalidToken()
        {
            //arrange
            MedicationController medicationController = badContext();
            string testName = "coffee";
            //act
            FindDrugResponse drug = medicationController.FindDrug(testName);
            //assert
            Assert.False(drug.success);
        }
        [Fact]
        public void ViewFavorites()
        {
            //arrange
            MedicationController medicationController = medicationContext();
            
            string testNDC = "123";
            string TestGeneric = "generic";
            string TestBrand = "brand";
            //act
            medicationController.AddFavorites(TestGeneric,TestBrand,testNDC);
            
            ViewFavoriteRequest drug = medicationController.ViewFavorites();
            

            //assert
            Assert.True(drug.isSuccess);
            //cleanup
            medicationController.RemoveFavorite(testNDC);
        }

    }
}