using System.Text.Json;
using The6Bits.BitOHealth.Models;

namespace The6Bits.API
{
    public class EdmamAPIHelper
    {

        public async Task<EdamamResponseRoot> GetRecommenedRecipes(DietR request)
        {
            var httpClient = new HttpClient();
            var endpoint = $"https://api.edamam.com/api/recipes/v2?app_id=d6503f85&app_key=08c1a71e5a883c74b3b4a652ee32840e&ingr={request.Ingr}&diet={request.Diet}&health={request.Health}&dishType={request.DishType}&calories={request.Calories}&cuisineType={request.CuisineType}&excluded={request.Excluded}&q={request.Q}&type=public";

            var response = await httpClient.GetAsync(endpoint);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<EdamamResponseRoot>(responseString);
            return result;
        }

        public async Task<Hit> GetRecommenedRecipeWithId(string recipeId)
        {
            var httpClient = new HttpClient();
            var endpoint = $"https://api.edamam.com/api/recipes/v2/{recipeId}?type=public&app_id=d6503f85&app_key=08c1a71e5a883c74b3b4a652ee32840e";

            var response = await httpClient.GetAsync(endpoint);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Hit>(responseString);

            return result;
        }



    }
}
