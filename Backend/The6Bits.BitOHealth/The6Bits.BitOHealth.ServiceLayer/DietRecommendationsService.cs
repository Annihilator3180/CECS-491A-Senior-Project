using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using The6Bits.API;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class DietRecommendationsService
    {
        private IDBErrors _DBErrors;
        private IConfiguration _config;
        private IRepositoryDietRecommendations _DietDao;
        public DietRecommendationsService(IRepositoryDietRecommendations DietDao, IDBErrors DbError)
        {
            _DietDao = DietDao; 
            _DBErrors = DbError;
        }

        public string SaveDietResponses(DietR d, string username)
        {
            string saveStatus = _DietDao.SaveDietResponses(d,username);
            if (saveStatus == "0")
            {
                return _DBErrors.DBErrorCheck(int.Parse(saveStatus));
            }
            else
            {
                return "Dietary Responses Saved";
            }
        }

        public async Task<string> getRecommendedRecipies(DietR responses)
        {
            EdmamAPIHelper helper = new EdmamAPIHelper();
            //contains found edamamirootresponse (including the hits)
            var result = await helper.GetRecommenedRecipes(responses);
            try
            {
                // List containing all the recipes retrieved from the api
                List<Recipe> recipe = result.hits.Select(_ => _.recipe).ToList();
                if(recipe.Count>0)
                {
                    string recipeList = JsonSerializer.Serialize(recipe);
                    return recipeList;

                }
                // convert json to string
                else
                {
                    return "No recipies found, please try again!";
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }


        }
        public async Task<string> getRecommendedRecipiesWithId(List<string> recipeIds)
        {
            EdmamAPIHelper helper = new EdmamAPIHelper();
            //contains found edamamirootresponse (including the hits)
            List<Recipe> recipeList = new List<Recipe>();
            foreach (string item in recipeIds)
            {
                var result = await helper.GetRecommenedRecipeWithId(item);
                recipeList.Add(result.recipe);
            }

            return JsonSerializer.Serialize(recipeList);

        }

        public async Task<String> AddToFavorite(FavoriteRecipe recipe, string username)  
        {
            return await _DietDao.AddToFavorite(recipe, username);
        }

        public async Task<string> DeleteFavorite(string recipeId)
        {
            return await _DietDao.DeleteFavorite(recipeId);
        }

        public async Task<List<string>> GetFavorites(string username)
        {
            return await _DietDao.GetFavorites(username);
        }

    }
}
