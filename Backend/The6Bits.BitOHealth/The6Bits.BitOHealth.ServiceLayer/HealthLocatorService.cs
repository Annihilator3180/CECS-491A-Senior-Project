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

    public HealthLocatorService()
    {

    }
    // Todo: Fix
    public async Task<string> viewHL()
    {
        string baseurl = "https://maps.googleapis.com/maps/api/staticmap?";
        //string baseurl = "https://www.google.com/maps/embed/v1/view";
        //https://maps.googleapis.com/maps/api/staticmap?center=Long+Beach,CA&zoom=14&size=400x400&key=YOUR_API_KEY&signature=YOUR_SIGNATURE
        //PARAM FOR SEARCH QUERY
        string qParam = "?center=Long+Beach,CA&zoom=14&size=400x400";

        var client = new HttpClient();

        //Header for API KEY

        client.DefaultRequestHeaders.Add("", apiKey);

        //SENT GET REQUEST W/ SEARCH PARAM ADDED
        var res = await client.GetAsync(baseurl + qParam);

        //GET jSON Data as String
        var contentString = await res.Content.ReadAsStringAsync();
        return contentString;
        
    }

    // Todo: Fix
    public async Task<string> searchHL()
    {
        string baseurl = "https://www.google.com/maps/embed/v1/search";

        //PARAM FOR SEARCH QUERY
        string qParam = "q=long+beach+state";

        var client = new HttpClient();


        client.DefaultRequestHeaders.Add("", apiKey);

        return "testing";
    }
}
