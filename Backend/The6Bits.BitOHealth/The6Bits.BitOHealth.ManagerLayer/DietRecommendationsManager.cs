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


        public DietRecommendationsManager(IRepositoryDietRecommendations DietDao, IAuthenticationService authenticationService, IDBErrors dbError, IConfiguration config)
        {
            _iDBErrors = dbError;
            _auth = authenticationService;
            _config = config;
            _DRS = new DietRecommendationsService(DietDao, dbError,config);
            
        }
        public string SaveDietRespones(DietR d)
        {
            return _DRS.SaveDietResponses(d);
        }

        public async Task<List<Recipe>> getRecommendedRecipies(DietR responses)
        {
            return await _DRS.getRecommendedRecipies(responses);
        }
    }
}
