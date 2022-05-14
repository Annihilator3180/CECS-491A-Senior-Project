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
    public class BMICalculatorService
    {
        private IDBErrors _DBErrors;
        private IConfiguration _config;
        private IRepositoryBMICalculator BMIDAO;
        public BMICalculatorService(IRepositoryBMICalculator _BMIDao, IDBErrors DbError)
        {
            BMIDAO = _BMIDao;
            _DBErrors = DbError;
        }
        public string GetBMI(double height, double weight)
        {
            height = height * 0.01;
            double bmi = (weight) / ((height) * (height));
            return bmi.ToString("#.##");
        }

        public string SaveBMI(string username, double height, double weight, string bmi)
        {
            string saveStatus = BMIDAO.SaveBMI(username, height, weight, bmi);
            if (saveStatus == "0")
            {
                return _DBErrors.DBErrorCheck(int.Parse(saveStatus));
            }
            else
            {
                return "User BMI Saved";
            }
        }





    }
}
