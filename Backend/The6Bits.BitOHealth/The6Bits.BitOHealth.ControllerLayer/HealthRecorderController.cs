﻿using Microsoft.AspNetCore.Mvc;
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
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("HealthRecorder")]
    public class HealthRecorderController : ControllerBase
    {
        private IAuthenticationService _authentication;
        private LogService logService;
        private HealthRecorderManager _HealthRecorderManager;

        public HealthRecorderController(IAuthenticationService authentication, ILogDal logDal, IDBErrors dBErrors, IRepositoryHealthRecorderDAO dao)
        {
            _authentication = authentication;
            logService = new LogService(logDal);
            _HealthRecorderManager = new HealthRecorderManager(dBErrors, dao);
        } 

        [HttpPost("CreateRecord")]
        //how can you pass 1 or 2 files
        public ActionResult CreateRecord([FromForm]string recordName, [FromForm]string categoryName, IFormFile file, IFormFile? file2)
        {
           
            string token = "";
            HealthRecorderResponseModel response = new HealthRecorderResponseModel();


            try
            {
                token = Request.Headers["Authorization"];

            }
            catch
            {
                response.ErrorMessage = "No Token";
                return Unauthorized(response);
            }
            token = token.Split(' ')[1];


            bool isValid = _authentication.ValidateToken(token);
           

            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                response.ErrorMessage = "Invalid Token";
                return Unauthorized(response);
            }

            string username = _authentication.getUsername(token);

            response = _HealthRecorderManager.CreateRecord(response, username, categoryName, recordName, file, file2);
            //ask in office hours how to include custom message

            if (response.ErrorMessage != null)
            {
                if (response.ErrorMessage.Contains("Database"))
                {
                    _ = logService.Log(username, response.ErrorMessage, "Database", "Buisness");
                    return StatusCode(500, response);
                }
                else
                {
                    _ = logService.Log(username, "User Error" + response.ErrorMessage, "Info", "Buisness");
                    return BadRequest(response);
                }
            }
            else
            {
                _ = logService.Log(username, response.Data, "Info", "Buisness");
                return Ok(response);
            }
            
        }
        [HttpGet("ViewRecord")]
        //https://stackoverflow.com/questions/381508/can-a-byte-array-be-written-to-a-file-in-c
        //db can have blob data type
        //to export pass in bytes and write bytes to path
        //lastRecordIndex in form data
        public ActionResult ViewRecord(int lastRecordIndex)
        {
            string token = "";
            HealthRecorderViewRecordModel response = new HealthRecorderViewRecordModel();

            try
            {
                token = Request.Headers["Authorization"];
            }
            catch
            {
                response.ErrorMessage = "No Token";
                return Unauthorized(response);
            }
            token = token.Split(' ')[1];


            bool isValid = _authentication.ValidateToken(token);


            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                response.ErrorMessage = "Invalid Token";
                return Unauthorized(response);
            }

            string username = _authentication.getUsername(token);
            response = _HealthRecorderManager.ViewRecord(username, lastRecordIndex);

          

            if (response.ErrorMessage != null)
            {
                _ = logService.Log(username, "DB Error", response.ErrorMessage, "Buisness");
                return StatusCode(500, response);
            }
            else if (response == null)
            {
                _ = logService.Log(username, "View Request success, 0 records outputted", "Info", "Buisness");
                return Ok(response);
            }
            else
            {
                _ = logService.Log(username, "View Request success, 10 or less outputted records ", "Info", "Buisness");
                return Ok(response);
            }
        }
        [HttpDelete("DeleteRecord")]
        public ActionResult DeleteRecord([FromBody]HealthRecorderRequestModel request)
        {
            string token = "";
            HealthRecorderResponseModel response = new HealthRecorderResponseModel();

            try
            {
                token = Request.Headers["Authorization"];
            }
            catch
            {
                response.ErrorMessage = "No Token";
                return Unauthorized(response);
            }
            token = token.Split(' ')[1];

            bool isValid = _authentication.ValidateToken(token);


            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                response.ErrorMessage = "Invalid Token";
                return Unauthorized(response);
            }

            string username = _authentication.getUsername(token);
            response = _HealthRecorderManager.DeleteRecord(request, response, username);

            if (response.ErrorMessage != null)
            {
                _ = logService.Log(username, "DB Error", response.ErrorMessage, "Buisness");
                return StatusCode(500, response);
            }
            else if (response.Data.Contains("Record Deleted Successfully"))
            {
                _ = logService.Log(username, response.Data + " " + request.RecordName + " " + request.CategoryName, "Info", "Buisness");
                return Ok(response);
            }
            else
            {
                _ = logService.Log(username, response.Data, "Info", "Buisness");
                return BadRequest(response);
            }
            

        }
        [HttpGet("SearchRecord")]
        //return action result instead, insert object into the action result
        public ActionResult SearchRecord(string recordName, string categoryName)
        {
            string token = "";
            HealthRecorderRequestModel request = new HealthRecorderRequestModel(recordName, categoryName);
            HealthRecorderViewRecordModel response = new HealthRecorderViewRecordModel();

            try
            {
                token = Request.Headers["Authorization"];
            }
            catch
            {
                response.ErrorMessage = "No Token";
                return Unauthorized(response);
            }
            token = token.Split(' ')[1];

            bool isValid = _authentication.ValidateToken(token);


            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                response.ErrorMessage = "Invalid Token";
                return Unauthorized(response);
            }

            string username = _authentication.getUsername(token);
            response = _HealthRecorderManager.SearchRecord(request, response, username);
            if (response.ErrorMessage != null)
            {
                _ = logService.Log(username, "DB Error", response.ErrorMessage, "Buisness");
                return StatusCode(500, response);
            }
            else if (response.Records.Any())
            {
                _ = logService.Log(username, "Records Succesfully Searched", "Info", "Buisness");
                return Ok(response);
            }
            else
            {
                _ = logService.Log(username, "Records Succesfully Searched, None outputted", "Info", "Buisness");
                return Ok(response);
            }
        }
        [HttpGet("ExportRecord")]
        public ActionResult ExportRecord( string categoryName, string recordName, string recordNumber)
        {
            string token = "";
            HealthRecorderExportModel response = new HealthRecorderExportModel();
            HealthRecorderRequestModel request = new HealthRecorderRequestModel(recordName, categoryName, recordNumber);

            try
            {
                token = Request.Headers["Authorization"];
            }
            catch
            {
                response.ErrorMessage = "No Token";
                return Unauthorized(response);
            }
            token = token.Split(' ')[1];

            bool isValid = _authentication.ValidateToken(token);


            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                response.ErrorMessage = "Invalid Token";
                return Unauthorized(response);
            }
            string username = _authentication.getUsername(token);
            response = _HealthRecorderManager.ExportRecord(request, response, username);

            if (response.ErrorMessage != null)
            {
                if (response.ErrorMessage.Contains("Database"))
                {
                    _ = logService.Log(username, "DB Error", response.ErrorMessage, "Buisness");
                    return StatusCode(500, response);
                }
                else
                {
                    _ = logService.Log(username, response.ErrorMessage, "Info", "Buisness");
                    return BadRequest(response);

                }
            }
            else
            {
                return Ok(response);
            }
            

        }
        [HttpPost("EditRecord")]
        public ActionResult EditRecord([FromForm] string newRecordName, [FromForm] string oldRecordName, [FromForm] string categoryName, IFormFile file, IFormFile? file2)
        {
            string token = "";
            HealthRecorderResponseModel response = new HealthRecorderResponseModel();

            try
            {
                token = Request.Headers["Authorization"];
            }
            catch
            {
                response.ErrorMessage = "No Token";
                return Unauthorized(response);
            }
            token = token.Split(' ')[1];

            bool isValid = _authentication.ValidateToken(token);


            if (!isValid)
            {
                _ = logService.Log("None", "Invalid Token @ Health Recorder", "Info", "Buisness");
                response.ErrorMessage = "Invalid Token";
                return Unauthorized(response);
            }
            string username = _authentication.getUsername(token);
            response = _HealthRecorderManager.EditRecord(response, username, categoryName, newRecordName, oldRecordName, file, file2);

            if (response.ErrorMessage != null)
            {
                if (response.ErrorMessage.Contains("Database"))
                {
                    _ = logService.Log(username, "DB Error @ Edit Record", response.ErrorMessage, "Buisness");
                    return StatusCode(500, response);
                }
                else
                {
                    _ = logService.Log(username, "no record updated", response.ErrorMessage, "Buisness");
                    return BadRequest(response);
                }
            }
            else
            {
                _ = logService.Log(username, "record updated " + newRecordName, "Info", "Buisness");

                return Ok(response);
            }
        }
       
         


    }
}
