using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.DAL
{
    public class OpenFDADAO : IDrugDataSet
    {

        private static HttpClient _httpClient;

        public OpenFDADAO(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
        }


        public async Task<List<DrugName>> GetGenericDrugName(string drugName)
        {
            string url = $"https://api.fda.gov/drug/ndc.json?api_key={Environment.GetEnvironmentVariable("OpenFda")}&search=generic_name:%22{drugName}%22&limit=3";
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(result);
                    var popupJson = doc.RootElement.GetProperty("results");
                    List<DrugName> values = JsonSerializer.Deserialize<List<DrugName>>(popupJson);
                    return values;
                }
                else
                {
                    DrugName emptyDrug = new DrugName();
                    List<DrugName> emptyGenericDrugsList= new List<DrugName>();
                    emptyGenericDrugsList.Add(emptyDrug);
                    return emptyGenericDrugsList;

                }
            }


        }
        public async Task<List<DrugName>> GetBrandDrugName(string drugName)
        {
            string url = $"https://api.fda.gov/drug/ndc.json?api_key={Environment.GetEnvironmentVariable("OpenFda")}&search=brand_name:%22{drugName}%22&limit=3";
            using (HttpResponseMessage response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(result);
                    var popupJson = doc.RootElement.GetProperty("results");
                    List<DrugName> values = JsonSerializer.Deserialize<List<DrugName>>(popupJson);
                    return values;
                }
                else
                {
                    DrugName emptyDrug = new DrugName();
                    List<DrugName> emptyGenericDrugsList = new List<DrugName>();
                    emptyGenericDrugsList.Add(emptyDrug);
                    return emptyGenericDrugsList;

                }
            }


        }

    }
}
