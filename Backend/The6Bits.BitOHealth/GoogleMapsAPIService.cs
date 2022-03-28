uusing System.Net.Http;
using System.Text.Json;
using FoodAPI.Contracts;
using The6Bits.BitOHealth.Models;


namespace MapAPI
{
    public class GoogleMapsAPIService<T> : IMapAPI<Parsed>
    {
        private readonly HttpClient _client;
        private readonly string _appId;
        private readonly string _appKey;

        public GoogleMapsAPIService(HttpClient httpClient, GoogleMapConfig config)
        {
            _client = httpClient;
            _appId = config.AppId;
            _appKey = config.AppKey;
        }

        public async Task<IEnumerable<Parsed>> QueryLocations(string queryString)
        {
            string url = "https://api.edamam.com/api/food-database/v2/parser?app_id="+ _appId + "&app_key="+ _appKey + "&ingr="+ queryString;
            //   <script async defer src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&callback=initMap"></script>
            var response = await _client.GetStringAsync(url);
            Root myDeserializedClass = JsonSerializer.Deserialize<Root>(response);

            return myDeserializedClass.parsed;
        }

    }
