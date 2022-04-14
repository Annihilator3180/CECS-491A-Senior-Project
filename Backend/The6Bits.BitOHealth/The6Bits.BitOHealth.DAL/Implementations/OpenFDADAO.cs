using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.DAL
{
    public class OpenFDADAO : IDrugDataSet
    {

        private static HttpClient _httpClient;

        public OpenFDADAO(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<DrugName>> GetGenericDrugName(string drugName)
        {
            string url = $"?api_key={Environment.GetEnvironmentVariable("OpenFda")}&search=generic_name:%22{drugName}%22&limit=3";
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
            string url = $"?api_key={Environment.GetEnvironmentVariable("OpenFda")}&search=brand_name:%22{drugName}%22&limit=3";
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
        public async Task<drugInfo> GetDrugInfo(string brand_name)
        {
            string url = $"https://api.fda.gov/drug/label.json?api_key={Environment.GetEnvironmentVariable("OpenFda")}&search=openfda.brand_name:%22{brand_name}%22&limit=1";
            using (var response = await _httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    drugInfos values = JsonSerializer.Deserialize<drugInfos>(await response.Content.ReadAsStringAsync());
                    try
                    {
                        return values.results[0];
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error getting drug information");
                    }
                }
                else
                {
                    throw new Exception("Error getting drug information");

                }
            }
        }

    }
}
