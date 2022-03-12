using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class HotTopicsService
    {
        private string apiKey;
        
        public HotTopicsService(string key)
        {
            apiKey = key;
        }

        public async Task<string> viewHT()
        {
            
            string baseurl = "https://api.bing.microsoft.com/v7.0/news/search";

            //PARAM FOR SEARCH QUERY
            string qParam = "?q=Trending News";


            var client = new HttpClient();

            //HEADER FOR API KEY 
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);


            //SENT GET REQUEST W SEARCH PARAM ADDED
            var res = await client.GetAsync(baseurl + qParam);

            //GET jSON DATA AS STRING
            var contentString = await res.Content.ReadAsStringAsync();
            return contentString;
        }
    }
}
