using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ManagerLayer;
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
        private WeightManagementManager _WMM;
        private LogService logService;

        private bool isValid;
        public WeightManagementController(IRepositoryWeightManagementDao dao, IAuthenticationService authentication, ILogDal logDal)
        {
            _dao = dao;
            _authentication = authentication;
            logService = new LogService(logDal);
            _WMM = new WeightManagementManager(dao);
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

                _ = logService.Log("None", "Invalid Token - Weight Goal", "Info", "Business");
                return "InvalidToken";

            }

            string username = _authentication.getUsername(token);

            string res = _WMM.CreateGoal(goalNum, username);

            if (res.Contains("Database"))
            {
                _ = logService.Log(username, "Create Weight Goal" + res, "DataStore", "Error");
            }

            _ = logService.Log(username, "Saved Weight Goal", "Info", "Business");


            return res;

        }




    }
}
