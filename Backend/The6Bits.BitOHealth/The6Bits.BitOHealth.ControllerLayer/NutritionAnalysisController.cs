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
    [Route("NutritionAnalysis")]
    public class NutritionAnalysisController : ControllerBase
    {
        private NutritionAnalysisManager _NIM;
        private LogService _logService;
        private IDBErrors _dBErrors;
        private IConfiguration _config;
        private IAuthenticationService _auth;
        private bool _isValid;

        public NutritionAnalysisController(IRepositoryNutritionAnalysis NADAO, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors)
        {
            _NIM = new NutritionAnalysisManager(NADAO, authenticationService, dbErrors);
            _logService = new LogService(logDao);
            _dBErrors = dbErrors;
            _auth = authenticationService;
        }

        [HttpPost("Create")]
        public async Task<object> CreateNutritionAnalysis([FromBody] Ingredients iingredients)
        {
            
            string? token = "";
            try
            {
                token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];
            }
            catch
            {
                return JsonSerializer.Serialize(new { success = false, message = "No Token" });
            }

            _isValid = _auth.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Nutritional Analysis", "Info", "Business");
                return JsonSerializer.Serialize(new { success = false, message = "Invalid Token" });
            }
                        string username = _auth.getUsername(token);

            string rec = _NIM.SaveRecipeRespones(username,iingredients);
            object response = await _NIM.GetNutritionAnalysis(iingredients);

            return response;

        }


    }
}
