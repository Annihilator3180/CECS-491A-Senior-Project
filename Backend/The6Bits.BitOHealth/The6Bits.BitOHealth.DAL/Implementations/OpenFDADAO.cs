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
        private IConfiguration _config;

        private static HttpClient api { get; set; }
        public OpenFDADAO(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<DrugName>> GetGenericDrugName(string drugName)
        {
            api = new HttpClient();
            api.DefaultRequestHeaders.Accept.Clear();
            api.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = "https://api.fda.gov/drug/ndc.json?api_key=imFf95grrUBnCaPj2DA3MQQtpCpBmnPFiTtXfbD8&search=generic_name:%22" +drugName+"%22&limit=3";
            using (HttpResponseMessage response = await api.GetAsync(url))
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
            api = new HttpClient();
            api.DefaultRequestHeaders.Accept.Clear();
            api.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = "https://api.fda.gov/drug/ndc.json?api_key=imFf95grrUBnCaPj2DA3MQQtpCpBmnPFiTtXfbD8&search=brand_name:%22" + drugName + "%22&limit=3";
            using (HttpResponseMessage response = await api.GetAsync(url))
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
