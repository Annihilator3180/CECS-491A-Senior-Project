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

        // Todo: Fix
        public GoogleMapsAPIService(HttpClient httpClient, GoogleMapConfig config)
        {
            _client = httpClient;
            _appId = config.AppId;
            _appKey = config.AppKey;
        }

        // Todo: Fix
        public async Task<IEnumerable<Parsed>> QueryLocations(string queryString)
        {
            string url = "https://www.google.com/maps/embed/v1/view?key="+ _appKey + "&" + queryString;
            //API Key: 
            //   <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA3ZXQrC6k4_x2gZ0x1wBtWCmm0BCP7dPs&callback=initMap"></script>
            var response = await _client.GetStringAsync(url);
            Root myDeserializedClass = JsonSerializer.Deserialize<Root>(response);

            return myDeserializedClass.parsed;
        }

    }
