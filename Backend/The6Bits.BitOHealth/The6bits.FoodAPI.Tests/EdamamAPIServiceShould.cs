using System.Net.Http;
using FoodAPI;
using The6Bits.BitOHealth.Models;
using Xunit;

namespace The6bits.FoodAPI.Tests
{
    public class EdamamApiServiceShould
    {
        //TODO:Dependency Inject API
        private EdamamAPIService<Parsed> service;
        public EdamamApiServiceShould()
        {

            service = new EdamamAPIService<Parsed>(new HttpClient(), new EdamamConfig());
        }


        [Fact]
        public void GetFood()
        {

        }
    }
}