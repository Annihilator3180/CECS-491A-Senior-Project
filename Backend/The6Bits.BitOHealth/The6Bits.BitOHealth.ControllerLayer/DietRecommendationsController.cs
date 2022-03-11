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


namespace The6Bits.BitOHealth.ControllerLayer
{
    public class DietRecommendationsController
    {
        private DietRecommendationsManager _DRM;
        private LogService _logService;
        private IDBErrors _dBErrors;
        private IConfiguration _config;
        private IAuthenticationService _auth;
        public DietRecommendationsController(ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors,
             IConfiguration config)
        {
            _DRM = new DietRecommendationsManager(authenticationService, dbErrors, config);
            _logService = new LogService(logDao);
            _dBErrors = dbErrors;
            _auth = authenticationService;
            _config = config;
        }

       // [HttpGet("Create")]
       // public List<Recipes> getRecommendedRecipies()
        



    }
}
