/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using The6Bits.DBErrors;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("NutritionalAnalysis")]
    public class NutritionAnalysisController
    {
       // private NutritionalAnalysisManager _DRM;
        private LogService _logService;
        private IDBErrors _dBErrors;
        private IConfiguration _config;
        private IAuthenticationService _auth;
        public NutritionalAnalysisController(IRepositoryNutritionalAnalysis DietDao, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors)
        {
            _DRM = new DietRecommendationsManager(DietDao, authenticationService, dbErrors);
            _logService = new LogService(logDao);
            _dBErrors = dbErrors;
            _auth = authenticationService;
        }
        private bool _isValid;
    }
}
*/