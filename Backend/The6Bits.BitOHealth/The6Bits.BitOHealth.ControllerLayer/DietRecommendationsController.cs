using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.Models;
using System.Text.Json;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.DAL.Implementations;

namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("DietRecommendation")]
    public class DietRecommendationsController : ControllerBase
    {
        private DietRecommendationsManager _DRM;
        private LogService _logService;
        private IDBErrors _dBErrors;
        private IConfiguration _config;
        private IAuthenticationService _auth;
        public DietRecommendationsController(IRepositoryDietRecommendations DietDao, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors)
        {
            _DRM = new DietRecommendationsManager(DietDao, authenticationService, dbErrors);
            _logService = new LogService(logDao);
            _dBErrors = dbErrors;
            _auth = authenticationService;
        }
        private bool _isValid;


        [HttpGet("Create")]
        public async Task<string> CreateDietRecommendations([FromQuery] DietR userResponses)
        {
            
                string? token = "";
                try
                {
                    token = Request.Headers["Authorization"];
                    token = token.Split(' ')[1];
                }
                catch
                {
                    return "No Token";
                }

                _isValid = _auth.ValidateToken(token);

                if (!_isValid)
                {

                    _ = _logService.Log("None", "Invalid Token - Diet Recommendations", "Info", "Business");
                    return "Invalid Token";
                }


             string username = _auth.getUsername(token);
             string response =  _DRM.SaveDietRespones(userResponses, username);    
             string recipes = await _DRM.getRecommendedRecipies(userResponses);
            // recipeList = JsonSerializer.Serialize(recipes);
            return recipes;

        }

        [HttpGet("GetRecipes")]
        public async Task<string> GetRecipeWithIds()
        {
            
            string? token = "";
            try
            {
                token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];
            }
            catch
            {
                return "No Token";
            }

            _isValid = _auth.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Diet Recommendations", "Info", "Business");
                return "Invalid Token";
            }

            string username = _auth.getUsername(token);
            List<string> recipeIds = _DRM.GetFavorites(username);
            string recipes = await _DRM.getRecommendedRecipiesWithId(recipeIds);
            return recipes;

        }

        [HttpGet("AddFavorite")]
        public string AddtoFavorite(string recipeId)
        {
           
            string? token = "";
            try
            {
                token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];
            }
            catch
            {
                return "No Token";
            }

            _isValid = _auth.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Diet Recommendations", "Info", "Business");
                return "Invalid Token";
            }

            string username = _auth.getUsername(token);
           
            FavoriteRecipe favoriteRecipe = new FavoriteRecipe(recipeId);
            string favoriteResult =  _DRM.AddToFavorite(favoriteRecipe,username);
            return favoriteResult;
        }

        [HttpGet("DeleteFavorite")]
        public string DeleteFavorite(string recipeId)
        {
            
            string? token = "";
            try
            {
                token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];
            }
            catch
            {
                return "No Token";
            }

            _isValid = _auth.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Diet Recommendations", "Info", "Business");
                return "Invalid Token";
            }
           
            FavoriteRecipe favoriteRecipe = new FavoriteRecipe(recipeId);
            string favoriteResult =  _DRM.DeleteFavorite(favoriteRecipe);
            return  favoriteResult;
        }

        [HttpGet("GetFavorites")]
        public List<string> GetFavorites()
        {
            
            string? token = "";
            /*
            try
            {
               
            }
            catch
            {
                return "No Token";
            }

            _isValid = _auth.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Diet Recommendations", "Info", "Business");
                return "Invalid Token";
            }

            */
            token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            string username = _auth.getUsername(token);
            return _DRM.GetFavorites(username);
        }
    }
}
