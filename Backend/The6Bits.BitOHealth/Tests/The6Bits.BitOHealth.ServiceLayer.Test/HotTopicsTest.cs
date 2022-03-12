using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace The6Bits.BitOHealth.ServiceLayer.Test
{
    public class HotTopicsTest
    {
        //[Theory]
        //[InlineData("https://api.bing.microsoft.com/v7.0/news/search")]
        [Fact]
        public async void TestNewsApiAsync()
        {
            // arrange
            HttpClient client = new HttpClient();
            string url = "https://api.bing.microsoft.com/v7.0/news/search";

            // act 
            var checkingResponse = await client.GetAsync(url);

            // assert
            if (checkingResponse.IsSuccessStatusCode)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(!checkingResponse.IsSuccessStatusCode);
            }
        }
    }
}
