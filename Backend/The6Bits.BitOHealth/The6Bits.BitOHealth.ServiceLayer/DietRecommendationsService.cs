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
    public class DietRecommendationsService
    {
        private IDBErrors _DBErrors;
        private IConfiguration _config;
        private IRepositoryDietRecommendations _DietDao;
        public DietRecommendationsService(IRepositoryDietRecommendations DietDao, IDBErrors DbError,IConfiguration config)
        {
            _DietDao = DietDao; 
            _DBErrors = DbError;
            _config = config;
        }

        public string SaveDietResponses(DietR d)
        {
            string saveStatus = _DietDao.SaveDietResponses(d);
            if (saveStatus == "0")
            {
                return _DBErrors.DBErrorCheck(int.Parse(saveStatus));
            }
            else
            {
                return "Dietary Responses Saved";
            }
        }

        public async Task<List<Recipe>> getRecommendedRecipies(DietR responses)
        {
            var helper = new EdmamAPIHelper();

            var result = await helper.GetRecommenedRecipes(responses);

            return result.hits.Select(_ => _.recipe).ToList();

        }
    }
}
