using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.Models.WeightManagement;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class WeightManagementService
    {




        private readonly IRepositoryWeightManagementDao<IWeightManagerResponse> _weightManagementDao;
        private readonly ILogDal _logDal;
        
        public WeightManagementService(IRepositoryWeightManagementDao<IWeightManagerResponse> dao, ILogDal logDal)
        {
            _logDal = logDal;
            _weightManagementDao = dao;
        }

        public async Task<IWeightManagerResponse> DeleteGoal(string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.Delete(username);
            if (res.IsError is not true) return res; 


            //ERROR CASE
            LogService logService = new LogService(_logDal);
            _ = logService.Log(username, (string)res.Result+" DeleteGoal", "DataStore", "Error");
            return res;
        }


        public async Task<IWeightManagerResponse> CreateGoal(GoalWeightModel goal, string username)
        {

            IWeightManagerResponse res = await _weightManagementDao.Create(goal,username);
            if (res.IsError is not true) return res;


            //ERROR CASE
            LogService logService = new LogService(_logDal);
            _ = logService.Log(username, (string)res.Result + " DeleteGoal", "DataStore", "Error");


            return res;

        }

        public async Task<IWeightManagerResponse> UpdateGoal(GoalWeightModel goal, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.Update(goal, username);
            if (res.IsError is not true) return res;



            //ERROR CASE
            LogService logService = new LogService(_logDal);
            _ = logService.Log(username, (string)res.Result + " UpdateGoal ", "DataStore", "Error");



            return res;
        }

        public async Task<IWeightManagerResponse> ReadGoal( string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.Read(username);
            if (res.IsError is not true) return res;


            //ERROR CASE
            LogService logService = new LogService(_logDal);
            _ = logService.Log(username, (string)res.Result + " ReadGoal ", "DataStore", "Error");



            return res;


        }



        public async Task<IWeightManagerResponse> StoreFoodLog(FoodModel food, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.CreateFoodLog(food, username);
            if (res.IsError is not true) return res;


            //ERROR CASE
            LogService logService = new LogService(_logDal);
            _ = logService.Log(username, (string)res.Result + " StoreFoodLog ", "DataStore", "Error");



            return res;

        }

        public async Task<IWeightManagerResponse> GetFoodLogs(string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.GetFoodLogs(username);
            if (res.IsError is not true) return res;


            //ERROR CASE
            LogService logService = new LogService(_logDal);
            _ = logService.Log(username, (string)res.Result + " GetFoodLogs ", "DataStore", "Error");



            return res;


        }

        public async Task<IWeightManagerResponse> GetFoodLogsAfter(DateTime dateTime, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.GetFoodLogsAfter(dateTime,username);
            if (res.IsError is not true) return res;


            //ERROR CASE
            LogService logService = new LogService(_logDal);
            _ = logService.Log(username, (string)res.Result + " GetFoodLogsAfter ", "DataStore", "Error");



            return res;
        }

        public async Task<IWeightManagerResponse> DeleteFoodLog(int id, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.DeleteFoodLog(id, username);
            if (res.IsError is not true) return res;


            //ERROR CASE
            LogService logService = new LogService(_logDal);
            _ = logService.Log(username, (string)res.Result + " DeleteFoodLog ", "DataStore", "Error");



            return res;
        }


    }
}
