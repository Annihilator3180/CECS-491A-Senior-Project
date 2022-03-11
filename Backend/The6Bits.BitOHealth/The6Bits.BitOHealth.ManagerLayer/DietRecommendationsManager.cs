using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using The6Bits.Authentication.Contract;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class DietRecommendationsManager
    {
        private IAuthenticationService _auth;
       // private DietRecommendationsService _DRS;
        private IDBErrors _iDBErrors;

        private IConfiguration _config;



        public DietRecommendationsManager(IAuthenticationService authenticationService, IDBErrors dbError, IConfiguration config)
        {
            _iDBErrors = dbError;
            _auth = authenticationService;
            _config = config;
         //   _MS = new MedicationService(_drugDataSet, dbError, config);
        }
    }
}
