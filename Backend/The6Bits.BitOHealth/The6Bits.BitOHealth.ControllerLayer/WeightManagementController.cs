using FoodAPI.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.Models.WeightManagement;
using The6Bits.DBErrors;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;
using System.IO;


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
        public WeightManagementController(IRepositoryWeightManagementDao<IWeightManagerResponse> dao, IAuthenticationService authentication, ILogDal logDal, IDBErrors dbErrors, IFoodAPI<Parsed> foodApi, IRepositoryWeightManagementImageDao<IWeightManagerResponse> imageDao)
        {
            _authentication = authentication;
            _logService = new LogService(logDal);
            _weightManagementManager = new WeightManagementManager(dao,dbErrors, foodApi, logDal,imageDao);
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


            IWeightManagerResponse res = await _weightManagementManager.CreateGoal(goal, username);


            //USER ERROR
            if (res.UserError is true) return BadRequest(res.Result);


            _ = _logService.Log(username, "Saved Weight Goal", "Info", "Business");

            //SERVER ERROR
            return Ok(res.Result);

        }


        [HttpGet("SearchFood")]
        public async Task<ActionResult> SearchFood(string queryString)
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





            try
            {
                IWeightManagerResponse res = await _weightManagementManager.SearchFood(queryString);

                //USER ERROR
                if (res.UserError is true) return BadRequest(res.Result);


                return Ok(res.Result);
            }
            catch (Exception ex)
            {
                //ERROR CASE
                _=_logService.Log(username, "Error Search Food API", "Business", "Error");
                return StatusCode(500);
            }

        }



        [HttpPost("UpdateGoal")]
        public async Task<ActionResult> UpdateGoal(GoalWeightModel goal)
        {


            string token = "";
            try
            {
                token = Request.Cookies["token"];
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


            IWeightManagerResponse res = await _weightManagementManager.UpdateGoal(goal, username);



            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);




            //SUCCESS
            return Ok(res.Result);
        }


        [HttpGet("ReadGoal")]
        public async Task<ActionResult> ReadGoal()
        {

            string token = "";
            try
            {
                token = Request.Cookies["token"];
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


            IWeightManagerResponse res = await _weightManagementManager.ReadGoal(username);



            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);




            //SUCCESS
            return Ok(res.Result);

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

            IWeightManagerResponse res = await _weightManagementManager.StoreFoodLog(food, username);


            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);




            //SUCCESS
            return Ok(res.Result);

        }


        [HttpGet("GetFoodLogs")]
        public async Task<ActionResult> GetFoodLogs(){

            string? token = "";
            token = Request.Headers["Authorization"];
            

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

            IWeightManagerResponse res = await _weightManagementManager.GetFoodLogs(username);

            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);




            //SUCCESS
            return Ok(res.Result);

        }

        [HttpGet("GetProfileInfo")]
        public async Task<ActionResult> GetProfileInfo()
        {

            string? token = "";
            token = Request.Headers["Authorization"];


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



            IWeightManagerResponse res = await _weightManagementManager.GetProfileInfo(username);


            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);




            //SUCCESS
            return Ok(res.Result);
        }




        [HttpGet("DeleteFoodLog")]
        public async Task<ActionResult> DeleteFoodLog(int id)
        {

            string? token = "";
            token = Request.Headers["Authorization"];


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



            IWeightManagerResponse res = await _weightManagementManager.DeleteFoodLog(id,username);


            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);




            //SUCCESS
            return Ok(res.Result);
        }
        
        
        [HttpGet("SaveImage")]
        public async Task<ActionResult> SaveImage(IFormFile file)
        {

            



            string username = "bossadmin12";



            IWeightManagerResponse res = await _weightManagementManager.SaveImage(file, username);

            //USER ERROR
            if(res.UserError is true) return BadRequest(res.Result);


            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);




            //SUCCESS
            return Ok(res.Result);
        }

        [HttpGet("DeleteImage")]
        public async Task<ActionResult> DeleteImage(int index)
        {





            string username = "bossadmin12";



            IWeightManagerResponse res = await _weightManagementManager.DeleteImage(index, username);


            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);




            //SUCCESS
            return Ok(res.Result);
        }


        [HttpGet("GetImage")]
        public async Task<ActionResult> GetImage(int index)
        {





            string username = "bossadmin12";



            IWeightManagerResponse res = await _weightManagementManager.GetImage(index, username);



            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);


            var dataBytes = await System.IO.File.ReadAllBytesAsync((string)res.Result);
            var dataStream = new MemoryStream(dataBytes);


            //GOOD 
            return Ok(dataStream);


        }

        [HttpGet("GetAllImageIds")]

        public async Task<ActionResult> GetAllImageIds()
        {



            string username = "bossadmin12";



            IWeightManagerResponse res = await _weightManagementManager.GetAllImageIds(username);



            //INTERNAL ERROR CASE
            if (res.IsError is true) return StatusCode(500);



            //GOOD 
            return Ok(res.Result);


        }


        public async Task<ActionResult> DeleteGoal()
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


            IWeightManagerResponse res = await _weightManagementManager.DeleteGoal(username);


            //USER ERROR
            if (res.UserError is true) return BadRequest(res.Result);


            _ = _logService.Log(username, "Saved Weight Goal", "Info", "Business");

            //SERVER ERROR
            return Ok(res.Result);

        }


    }
}
