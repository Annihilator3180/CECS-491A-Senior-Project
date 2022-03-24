﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodAPI;
using FoodAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ControllerLayer.Features
{
    [ApiController]
    [Route("WeightManagement")]
    public class WeightManagementController : ControllerBase
    {

        private IRepositoryWeightManagementDao _dao;
        private IAuthenticationService _authentication;
        private WeightManagementManager _weightManagementManager;
        private LogService _logService;

        private bool isValid;
        public WeightManagementController(IRepositoryWeightManagementDao dao, IAuthenticationService authentication, ILogDal logDal, IDBErrors dbErrors, IFoodAPI<Parsed> foodApi)
        {
            _dao = dao;
            _authentication = authentication;
            _logService = new LogService(logDal);
            _weightManagementManager = new WeightManagementManager(dao,dbErrors, foodApi);
        }


        [HttpPost("CreateGoal")]

        public string CreateGoal(int goalNum)
        {
            string token = "";
            try
            {
                token = Request.Cookies["token"];
            }
            catch
            {
                return "NoToken";
            }

            isValid = _authentication.ValidateToken(token);

            if (!isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Weight Goal", "Info", "Business");
                return "InvalidToken";

            }

            string username = _authentication.getUsername(token);

            string res = _weightManagementManager.CreateGoal(goalNum, username);

            if (res.Contains("Database"))
            {
                _ = _logService.Log(username, "Create Weight Goal" + res, "DataStore", "Error");
                return res;
            }

            _ = _logService.Log(username, "Saved Weight Goal", "Info", "Business");


            return res;

        }


        [HttpGet("SearchFood")]
        public async Task<ActionResult> SearchFood(string queryString)
        {

            return  Ok(await _weightManagementManager.SearchFood(queryString));

        }




    }
}
