using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.ServiceLayer;
public class HealthLocatorService
{
    private string apiKey;

    // Todo: Fix
    public HealthLocatorService(string key)
    {
        apiKey = key;
    }

    // Todo: Fix
    public async Task<string> viewHL()
    {
        string baseurl = "";

        //PARAM FOR SEARCH QUERY
        string qParam = "";

        var client = new HttpClient();

        //Header for API KEY

        client.DefaultRequestHeaders.Add("", apiKey);

        //SENT GET REQUEST W/ SEARCH PARAM ADDED
        var res = await client.GetAsync(baseurl + qParam);

        //GET jSON Data as String
        var contentString = await res.Content.ReadAsStringAsync();
        return contentString;
    }
}
