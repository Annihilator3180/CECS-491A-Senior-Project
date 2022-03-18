using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.DBErrors;
using The6Bits.BitOHealth.DAL.Contract;

using The6Bits.Logging.Implementations;
using The6Bits.Logging.DAL.Contracts;
using Microsoft.AspNetCore.Http;

namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("HealthRecorder")]
    public class HealthRecorderController : ControllerBase
    {
        private IAuthenticationService _authentication;
        private LogService logService;
        private HealthRecorderManager _HRM;
        private IRepositoryHealthRecorderDAO _dao;

        public HealthRecorderController(IAuthenticationService authentication, ILogDal logDal, IDBErrors dBErrors, IRepositoryHealthRecorderDAO dao)
        {
            _authentication = authentication;
            logService = new LogService(logDal);
            _HRM = new HealthRecorderManager(dBErrors, dao);
            _dao = dao;
        }

        [HttpPost("CreateRecord")]
        //how can you pass 1 or 2 files
        public string CreateRecord(string recordName, string categoryName, IFormFile file, IFormFile? file2)
        {
           
            var test = HttpContext.Request.Form["file"];
            //starts here
            string token = "";
            DateTime dateTime = DateTime.Now;

            try
            {
                token = Request.Cookies["token"];
            }
            catch
            {
                return "No Token";
            }

            bool isValid = _authentication.ValidateToken(token);
           

            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                return "Invalid Token";
            }

            string username = _authentication.getUsername(token);

            string createRecord = _HRM.CreateRecord(username, dateTime, categoryName, recordName, file, file2);
            //end here
           
            return createRecord;
        }


    }
}
