using System;
using System.Linq;
using System.Net.Http;
using FoodAPI;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.Models;
using Xunit;

namespace The6bits.FoodAPI.Tests
{
    public class EdamamApiServiceShould : TestsBase
    {
        private EdamamAPIService<Parsed> service;
        public EdamamApiServiceShould()
        {

            service = new EdamamAPIService<Parsed>(new HttpClient(), edmamConfig);
        }


        [Theory(Timeout=5000)]
        [InlineData("pizza")]
        [InlineData("coke")]
        [InlineData("lemonade")]

        public void GetFood(string food)
        {
            var s = service.QueryFoods(food);
            Assert.NotNull(s.Result.First().food);
        }
        
    }
}