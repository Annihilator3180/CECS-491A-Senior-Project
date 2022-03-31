using System.Net.Http;
using System.Text.Json;
using FoodAPI.Contracts;
using The6Bits.BitOHealth.Models;


namespace FoodAPI
{
    public class EdamamAPIService<T> : IFoodAPI<Parsed>
    {
        private readonly HttpClient _client ;
        private readonly string _appId;
        private readonly string _appKey;

        public EdamamAPIService(HttpClient httpClient, EdamamConfig config)
        {
            _client = httpClient;
            _appId = config.AppId;
            _appKey = config.AppKey;
        }

        public async Task<IEnumerable<Parsed>> QueryFoods(string queryString)
        {
            string url = "https://api.edamam.com/api/food-database/v2/parser?app_id="+ _appId + "&app_key="+ _appKey + "&ingr="+ queryString;
            var response = await _client.GetStringAsync(url);
            Root myDeserializedClass = JsonSerializer.Deserialize<Root>(response);

            return myDeserializedClass.parsed;
        }

    }
}