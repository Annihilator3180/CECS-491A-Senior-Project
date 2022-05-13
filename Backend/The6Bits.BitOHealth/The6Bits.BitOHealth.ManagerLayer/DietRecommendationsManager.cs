using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class DietRecommendationsManager
    {
        private IAuthenticationService _auth;
        private DietRecommendationsService _DRS;
        private IDBErrors _iDBErrors;

        private IConfiguration _config;


        public DietRecommendationsManager(IRepositoryDietRecommendations DietDao, IAuthenticationService authenticationService, IDBErrors dbError)
        {
            _iDBErrors = dbError;
            _auth = authenticationService;
            _DRS = new DietRecommendationsService(DietDao, dbError);
            
        }
        public string SaveDietRespones(DietR d, string username)
        {
            return _DRS.SaveDietResponses(d, username);
        }

        public async Task<string> getRecommendedRecipies(DietR responses)
        {
            return await _DRS.getRecommendedRecipies(responses);
        }
        public string AddToFavorite(FavoriteRecipe favoriteRecipe, string username)
        {
            try
            {
                             
                 return _DRS.AddToFavorite(favoriteRecipe, username);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        public string DeleteFavorite(FavoriteRecipe favoriteRecipe)
        {
            try
            {
               return _DRS.DeleteFavorite(favoriteRecipe);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<string> GetFavorites( string username)
        {
            List<string> favs = new List<string>();
            try
            {
                return _DRS.GetFavorites(username);
            }
            catch (Exception ex)
            {
                return favs;
            }
        }

        public async Task<string> getRecommendedRecipiesWithId(List<string> recipeIds)
        {
            return await _DRS.getRecommendedRecipiesWithId(recipeIds);
        }
        

    }
}
