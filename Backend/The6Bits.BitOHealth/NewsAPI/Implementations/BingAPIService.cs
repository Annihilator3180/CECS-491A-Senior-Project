using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using NewsAPI.Contracts;
using The6Bits.BitOHealth.Models;

namespace NewsAPI
{
    public class BingAPIService<T> : INewsAPI<Parsed>
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly string _appId;

        public BingAPIService(HttpClient httpClient, BingConfig config)
        {
            _client = httpClient;
            _appId = config.AppId;
            _appKey = config.AppKey;
        }

        public async Task<IEnumerable<Parsed>> QueryNews(string queryString)
        {
            string url = "";
            var response = await _client.GetStringAsync(url);
            Root2 myDeserializedClass = JsonConvert.DeserializeObject<Root2>(myJsonResponse);

            return myDeserializedClass.parsed;
        }
    }
}
