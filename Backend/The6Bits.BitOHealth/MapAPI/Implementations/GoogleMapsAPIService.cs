﻿using System.Net.Http;
using System.Text.Json;
using MapAPI.Contracts;
using The6Bits.BitOHealth.Models;


namespace MapAPI
{
    public class GoogleMapsAPIService<T> : IMapAPI<Parsed>
    {
        private readonly HttpClient _client;
        
        private readonly string _appKey;
        
        // Todo: Fix
        public GoogleMapsAPIService(HttpClient httpClient, GoogleMapsConfig config)
        {
            _client = httpClient;
            
            _appKey = config.AppKey;
        }

        // Todo: Fix
        public async Task<IEnumerable<Parsed>> QueryLocations(string queryString)
        {
            string url = "https://maps.googleapis.com/maps/api/staticmap?center=" + queryString + "&zoom=14&size=400x400&key=" + _appKey;
            
            //?center= () this will be the search location
            //string url = "https://www.google.com/maps/embed/v1/search?q="+ queryString + "&key=" + _appKey;
            //API Key: 
            //   <script async defer src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&callback=initMap"></script>
            var response = await _client.GetStringAsync(url);
            Root myDeserializedClass = JsonSerializer.Deserialize<Root>(response);

            return myDeserializedClass.parsed;
        }

    }
}