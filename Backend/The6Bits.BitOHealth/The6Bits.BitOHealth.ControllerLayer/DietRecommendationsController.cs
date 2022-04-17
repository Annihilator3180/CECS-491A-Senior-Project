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

        [HttpGet("Create")]
        public async Task<string> CreateDietRecommendations([FromQuery] DietR userResponses)
        {
            string token = "";
            try
            {
                token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            }
            catch
            {
                return "NoToken";
            }
            bool isValid = _auth.ValidateToken(token);
            if (isValid != true)
            {
                _logService.Log("None", "Invalid Token - Create Diet Recommendation", "Info", "Bussiness");
                return "Invalid Token";
            }
            string username = _auth.getUsername(token);
            string response = _DRM.SaveDietRespones(userResponses);
            if (response.Contains("Database"))
            {
                _logService.Log("NO", "Save User Diet " + response, "DataStore", "Error");
            }

                _logService.Log("NO", "Save User Diet", "Info", "Business");

            List<Recipe> recipes = await _DRM.getRecommendedRecipies(userResponses);
            string recipeList = JsonSerializer.Serialize(recipes);
            return recipeList;
            bool isEmpty = !recipeList.Any();
            if (isEmpty)
            {
                return "No search results found";
            }


        }
    }
}
