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
    public class NutritionAnalysisManager
    {
        private IAuthenticationService _auth;
        private ServiceLayer.NutritionAnalysisService _NAS;
        private IDBErrors _iDBErrors;

        private IConfiguration _config;


        public NutritionAnalysisManager(IRepositoryNutritionAnalysis NADAO, IAuthenticationService authenticationService, IDBErrors dbError)
        {
            _iDBErrors = dbError;
            _auth = authenticationService;
            _NAS = new NutritionAnalysisService(NADAO, dbError);

        }
        public string SaveRecipeRespones(string username, Ingredients ingredients)
        {
            return _NAS.SaveRecipeResponse(username, ingredients);
        }



        public async Task<object> GetNutritionAnalysis(Ingredients responses)
        {
            var result = await _NAS.GetNutritionAnalysis(responses);
            return result;

        }

    }
}
