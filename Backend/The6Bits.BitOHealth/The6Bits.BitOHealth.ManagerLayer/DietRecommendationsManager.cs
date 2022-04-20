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
        public async Task<string> AddToFavorite(FavoriteRecipe recipe, string username)
        {
            try
            {
                await _DRS.AddToFavorite(recipe, username);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Favorited";
            //return await Task.FromResult("Hello");
        }

        public async Task<string> DeleteFavorite(string recipeId)
        {
            try
            {
               await _DRS.DeleteFavorite(recipeId);
            }
            catch (Exception ex)
            {
                return "Database Error";
            }
            return "Deleted";
            //return await Task.FromResult("Hello");
        }

        public async Task<List<string>> GetFavorites( string username)
        {
            List<string> favs = new List<string>();
            try
            {
                return await _DRS.GetFavorites(username);
            }
            catch (Exception ex)
            {
                return favs;
            }
        }

    }
}
