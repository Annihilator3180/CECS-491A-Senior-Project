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
    public class BMICalculatorManager
    {
        private IAuthenticationService _auth;
        private ServiceLayer.BMICalculatorService _BMIS;
        private IDBErrors _iDBErrors;

        private IConfiguration _config;


        public BMICalculatorManager(IRepositoryBMICalculator BMIDAO, IAuthenticationService authenticationService, IDBErrors dbError)
        {
            _iDBErrors = dbError;
            _auth = authenticationService;
            _BMIS = new BMICalculatorService(BMIDAO, dbError);

        }
        public string GetBMI(double height, double weight)
        {
            try
            {
                return _BMIS.GetBMI(height, weight);

            }catch (Exception ex)
            {
                return ex.Message;  
            }
        }

        public string SaveBMI(string username, double height, double weight, string bmi)
        {
            try
            {
                return _BMIS.SaveBMI(username, height, weight, bmi);

            }catch(Exception ex)
            {
                return ex.Message;  
            }
        }




    }
}
