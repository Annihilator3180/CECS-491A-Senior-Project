using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using The6Bits.API;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class NutritionAnalysisService
    {
        private IDBErrors _DBErrors;
        private IConfiguration _config;
        private IRepositoryNutritionAnalysis _NADAO;
        public NutritionAnalysisService(IRepositoryNutritionAnalysis nutritionAnalysis, IDBErrors DbError)
        {
            _NADAO = nutritionAnalysis;
            _DBErrors = DbError;
        }
        public string SaveRecipeResponse(string username, Ingredients ingredients)
        {
            string saveStatus = _NADAO.SaveRecipeResponse(username,ingredients);
            if (saveStatus == "0")
            {
                return _DBErrors.DBErrorCheck(int.Parse(saveStatus));
            }
            else
            {
                return "Dietary Responses Saved";
            }
        }
        public async Task<object> GetNutritionAnalysis(Ingredients responses)
        {
            try
            {
                var helper = new EdmamAPIHelper();

                var result = await helper.GetNutritionAnalysis(responses);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}
