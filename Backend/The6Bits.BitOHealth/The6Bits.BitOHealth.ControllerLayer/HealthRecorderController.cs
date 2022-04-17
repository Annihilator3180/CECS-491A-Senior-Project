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
using System.Net;

namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("HealthRecorder")]
    public class HealthRecorderController : ControllerBase
    {
        private IAuthenticationService _authentication;
        private LogService logService;
        private HealthRecorderManager _HealthRecorderManager;
        private IRepositoryHealthRecorderDAO _dao;

        public HealthRecorderController(IAuthenticationService authentication, ILogDal logDal, IDBErrors dBErrors, IRepositoryHealthRecorderDAO dao)
        {
            _authentication = authentication;
            logService = new LogService(logDal);
            _HealthRecorderManager = new HealthRecorderManager(dBErrors, dao);
            _dao = dao;
        }

        [HttpPost("CreateRecord")]
        //how can you pass 1 or 2 files
        public HttpResponseMessage CreateRecord([FromForm]string recordName, [FromForm]string categoryName, IFormFile file, IFormFile? file2)
        {
           
            var test = HttpContext.Request.Form["file"];
            //starts here
            string token = "";
            DateTime dateTime = DateTime.Now;

            try
            {
                token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            bool isValid = _authentication.ValidateToken(token);
           

            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            string username = _authentication.getUsername(token);

            string createRecord = _HealthRecorderManager.CreateRecord(username, dateTime, categoryName, recordName, file, file2);
            //ask in office hours how to include custom message

            if (createRecord.Contains("Database"))
            {
                _ = logService.Log(username, "Database Error" + createRecord, "Database", "Buisness");
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            if (createRecord != "Record Saved")
            {
                _ = logService.Log(username, "User Error" + createRecord, "Info", "Buisness");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            }
            else
            {
                _ = logService.Log(username, createRecord, "Info", "Buisness");

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

        }


    }
}
