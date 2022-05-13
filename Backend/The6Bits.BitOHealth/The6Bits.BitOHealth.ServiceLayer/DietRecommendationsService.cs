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
            var result = await helper.GetRecommenedRecipes(responses);
            try
            {
                List<Recipe> recipe = result.hits.Select(_ => _.recipe).ToList();

                if(recipe.Count>0)
                {
                    string recipeList = JsonSerializer.Serialize(recipe);
                    return recipeList;
                }
                else
                {
                    return JsonSerializer.Serialize(new { success = false, message = "No recipies found, please try again!" }) ;
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
                List<Recipe> recipeList = new List<Recipe>();
                foreach (string recipe in recipeIds)
                {
                    var result = await helper.GetRecommenedRecipeWithId(recipe);
                    recipeList.Add(result.recipe);

                }
                return JsonSerializer.Serialize(recipeList);
        }

        public string AddToFavorite(FavoriteRecipe favoriteRecipe, string username)  
        {
            List<string> favs = GetFavorites(username);

            if (favs.Contains(favoriteRecipe.Recipe_id))
            {
                  return JsonSerializer.Serialize(new { success = false, message = "Reciple already added!" });
            }
            else
            {
                return _DietDao.AddToFavorite(favoriteRecipe, username);
            }

        }

        public string DeleteFavorite(FavoriteRecipe favoriteRecipe)
        {
            return _DietDao.DeleteFavorite(favoriteRecipe);
        }

        public List<string> GetFavorites(string username)
        {
            return  _DietDao.GetFavorites(username);
        }

    }
}
