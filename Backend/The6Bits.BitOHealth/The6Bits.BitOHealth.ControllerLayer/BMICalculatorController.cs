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
    [Route("BMICalculator")]
    public class BMICalculatorController : ControllerBase
    {
        private BMICalculatorManager _BMIM;
        private LogService _logService;
        private IDBErrors _dBErrors;
        private IAuthenticationService _auth;
        private IConfiguration _config;
        private bool _isValid;

        public BMICalculatorController(IRepositoryBMICalculator _BMIDao, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors)
        {

            _BMIM = new BMICalculatorManager(_BMIDao, authenticationService, dbErrors);
            _logService = new LogService(logDao);
            _dBErrors = dbErrors;
            _auth = authenticationService;
        }

        [HttpPost("Calculate")]
        public async Task<string> BMICalculator(double height, double weight)
        {
            
            string response = "";
            

            string? token = "";
            try
            {
                token = Request.Headers["Authorization"];
                //   token = token.Split(' ')[1];
            }
            catch
            {
                return JsonSerializer.Serialize(new { success = false, message = "No Token" });
            }

            _isValid = _auth.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Nutrition Analysis", "Info", "Business");
                return JsonSerializer.Serialize(new { success = false, message = "Invalid Token" });
            }
            string username = _auth.getUsername(token);
           

            try
            {

                response = _BMIM.GetBMI(height, weight);
                string save = _BMIM.SaveBMI("emily", height, weight, response);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //var stringPayload = JsonSerializer.Deserialize<object>(response);
            return response;

        }


    }
}
