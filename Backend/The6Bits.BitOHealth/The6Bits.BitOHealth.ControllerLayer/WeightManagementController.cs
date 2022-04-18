using FoodAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ControllerLayer
{
    [ApiController]
    [Route("WeightManagement")]
    public class WeightManagementController : ControllerBase
    {

        private readonly  IAuthenticationService _authentication;
        private readonly WeightManagementManager _weightManagementManager;
        private readonly LogService _logService;

        private bool _isValid;
        public WeightManagementController(IRepositoryWeightManagementDao dao, IAuthenticationService authentication, ILogDal logDal, IDBErrors dbErrors, IFoodAPI<Parsed> foodApi)
        {
            _authentication = authentication;
            _logService = new LogService(logDal);
            _weightManagementManager = new WeightManagementManager(dao,dbErrors, foodApi);
        }


        [HttpPost("CreateGoal")]

        public async Task<ActionResult> CreateGoal(GoalWeightModel goal)
        {
            string? token = "";
            try
            {
                token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];
            }
            catch
            {
                return BadRequest("No Token");
            }

            _isValid = _authentication.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Weight Goal", "Info", "Business");
                return BadRequest("Invalid Token");
            }




            string username = _authentication.getUsername(token);

            string res = _weightManagementManager.CreateGoal(goal, username);

            if (res.Contains("Database"))
            {
                _ = _logService.Log(username, "Create Weight Goal" + res, "DataStore", "Error");
                return Ok(res);
            }

            _ = _logService.Log(username, "Saved Weight Goal", "Info", "Business");


            return Ok(res);

        }


        [HttpGet("SearchFood")]
        public async Task<ActionResult> SearchFood(string queryString)
        {

            return  Ok(await _weightManagementManager.SearchFood(queryString));

        }



        [HttpPost("UpdateGoal")]
        public async Task<ActionResult> UpdateGoal(GoalWeightModel goal)
        {


            string token = "";
            try
            {
                token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            }
            catch
            {
                return BadRequest("No Token");
            }

            _isValid = _authentication.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Weight Goal", "Info", "Business");
                return BadRequest("Invalid Token");
            }




            string username = _authentication.getUsername(token);

            return Ok(await _weightManagementManager.UpdateGoal(goal, username));

        }


        [HttpGet("ReadGoal")]
        public async Task<ActionResult> ReadGoal()
        {

            string token = "";
            try
            {
                token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            }
            catch
            {
                return BadRequest("No Token");
            }

            _isValid = _authentication.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Weight Goal", "Info", "Business");
                return BadRequest("Invalid Token");
            }




            string username = _authentication.getUsername(token);
            GoalWeightModel goal = await _weightManagementManager.ReadGoal(username);


            //TODO:Better way to do this?
            if (goal.GoalWeight == null)
            {
                return Ok("{}");
            }

            return Ok(goal);

        }


        [HttpPost("SaveFood")]
        public async Task<ActionResult> StoreFoodLog(FoodModel food)
        {


            string token = "";
            try
            {
                token = Request.Headers["Authorization"];
                token = token.Split(' ')[1];
            }
            catch
            {
                return BadRequest("No Token");
            }

            _isValid = _authentication.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Weight Goal", "Info", "Business");
                return BadRequest("Invalid Token");
            }




            string username = _authentication.getUsername(token);

            return Ok(await _weightManagementManager.StoreFoodLog(food, username));

        }

        [HttpGet("GetFoodLogs")]
        public async Task<ActionResult> GetFoodLogs()
        {


            string token = "";
            try
            {
                token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            }
            catch
            {
                return BadRequest("No Token");
            }

            _isValid = _authentication.ValidateToken(token);

            if (!_isValid)
            {

                _ = _logService.Log("None", "Invalid Token - Weight Goal", "Info", "Business");
                return BadRequest("Invalid Token");
            }




            string username = _authentication.getUsername(token);

            return Ok(await _weightManagementManager.GetFoodLogs(username));

        }



    }
}
