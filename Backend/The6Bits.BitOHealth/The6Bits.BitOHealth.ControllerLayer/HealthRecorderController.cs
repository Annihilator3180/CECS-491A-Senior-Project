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
using The6Bits.BitOHealth.Models;

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
                token = Request.Cookies["token"];
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
        [HttpGet("ViewRecord")]
        //https://stackoverflow.com/questions/381508/can-a-byte-array-be-written-to-a-file-in-c
        //db can have blob data type
        //to export pass in bytes and write bytes to path
        public string ViewRecord([FromForm] int lastRecordIndex)
        {
            string token = "";
            HealthRecorderViewRecordModel response = new HealthRecorderViewRecordModel();

            try
            {
                token = Request.Cookies["token"];
            }
            catch
            {
                response.HttpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.ErrorMessage = "No Token";
                return response.ToString();
            }

            bool isValid = _authentication.ValidateToken(token);


            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                response.HttpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.ErrorMessage = "Invalid Token";
                return response.ToString();
            }

            string username = _authentication.getUsername(token);
            response = _HealthRecorderManager.ViewRecord(username, lastRecordIndex);

            if (response == null)
            {
                _ = logService.Log(username, "View Request success, 0 records outputted", "Info", "Buisness");
                response.HttpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            }

            else if (response.ErrorMessage != null)
            {
                _ = logService.Log(username, "DB Error", response.ErrorMessage, "Buisness");
                response.HttpResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            else
            {
                _ = logService.Log(username, "View Request success, 10 or less outputted records ", "Info", "Buisness");
                response.HttpResponse = new HttpResponseMessage(HttpStatusCode.OK);
            }
            return response.ToString();
        }
         


    }
}
