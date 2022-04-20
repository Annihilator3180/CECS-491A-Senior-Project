using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.Models.WeightManagement;
using The6Bits.DBErrors;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class WeightManagementService
    {




        private readonly IRepositoryWeightManagementDao<IWeightManagerResponse> _weightManagementDao;
        private readonly ILogDal _logDal;
        private readonly IRepositoryWeightManagementImageDao<IWeightManagerResponse> _imageDao;
        private readonly IDBErrors _dbErrors;
        private readonly LogService _logService;

        public WeightManagementService(IRepositoryWeightManagementDao<IWeightManagerResponse> dao, ILogDal logDal ,IDBErrors dbErrors ,IRepositoryWeightManagementImageDao<IWeightManagerResponse> imageDao)
        {
            _logDal = logDal;
            _imageDao = imageDao;
            _weightManagementDao = dao;
            _dbErrors = dbErrors;
            _logService = new LogService(_logDal);
        }

        public async Task<IWeightManagerResponse> DeleteGoal(string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.Delete(username);
            if (res.IsError is not true) {
                _ = _logService.Log(username, " Success DeleteGoal", "Business", "Informational");
                return res;
            }


            //ERROR CASE
            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result)+" DeleteGoal", "DataStore", "Error");
            return res;
        }


        public async Task<IWeightManagerResponse> CreateGoal(GoalWeightModel goal, string username)
        {

            IWeightManagerResponse res = await _weightManagementDao.Create(goal,username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success CreateGoal", "Business", "Informational");
                return res;
            }


            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " CreateGoal", "DataStore", "Error");


            return res;

        }

        public async Task<IWeightManagerResponse> UpdateGoal(GoalWeightModel goal, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.Update(goal, username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success UpdateGoal", "Business", "Informational");
                return res;
            }


            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " UpdateGoal ", "DataStore", "Error");



            return res;
        }

        public async Task<IWeightManagerResponse> ReadGoal( string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.Read(username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success ReadGoal", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " ReadGoal ", "DataStore", "Error");



            return res;


        }



        public async Task<IWeightManagerResponse> StoreFoodLog(FoodModel food, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.CreateFoodLog(food, username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success StoreFoodLog", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " StoreFoodLog ", "DataStore", "Error");



            return res;

        }

        public async Task<IWeightManagerResponse> GetFoodLogs(string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.GetFoodLogs(username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success GetFoodLogs", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " GetFoodLogs ", "DataStore", "Error");



            return res;


        }

        public async Task<IWeightManagerResponse> GetFoodLogsAfter(DateTime dateTime, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.GetFoodLogsAfter(dateTime,username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success GetFoodLogsAfter", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " GetFoodLogsAfter ", "DataStore", "Error");



            return res;
        }

        public async Task<IWeightManagerResponse> DeleteFoodLog(int id, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.DeleteFoodLog(id, username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success DeleteFoodLog", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " DeleteFoodLog ", "DataStore", "Error");



            return res;
        }



        public async Task<IWeightManagerResponse> SaveImagePath(string path,string username)
        {

            IWeightManagerResponse res = await _weightManagementDao.SaveImagePath(path,DateTime.UtcNow , username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success SaveImagePath", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " SaveImagePath ", "DataStore", "Error");



            return res;
        }


        public async Task<IWeightManagerResponse> DeleteImagePath(int index, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.DeleteImagePath(index, username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success DeleteImagePath", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " DeleteImagePath ", "DataStore", "Error");



            return res;
        }

        public async Task<IWeightManagerResponse> GetImage(int index, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.GetImage(index, username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success GetImage", "Business", "Informational");
                return res;
            }






            //ERROR CASE
            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " GetTenImagesPath ", "DataStore", "Error");



            return res;
        }

        public async Task<IWeightManagerResponse> SaveImage(IFormFile imageFile, string username)
        {
            IWeightManagerResponse res = await _imageDao.SaveImage(imageFile, username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success SaveImage", "Business", "Informational");
                return res;
            }


            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " SaveImage ", "DataStore", "Error");



            return res;
        }


        public async Task<IWeightManagerResponse> GetAllImageIds( string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.GetAllImageIDs( username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success GetAllImageIds", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " GetAllImageIds ", "DataStore", "Error");



            return res;
        }

        public async Task<IWeightManagerResponse> GetFoodLogsAfterAddTime(DateTime date, string username)
        {
            IWeightManagerResponse res = await _weightManagementDao.GetFoodLogsAfterAddTime(date,username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success GetFoodLogsAfterAddTime", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " GetAllImageIds ", "DataStore", "Error");



            return res;
        }


        public async Task<IWeightManagerResponse> DeleteImage(string path, string username)
        {

            IWeightManagerResponse res = await _imageDao.DeleteImage(path, username);
            if (res.IsError is not true)
            {
                _ = _logService.Log(username, " Success DeleteImage", "Business", "Informational");
                return res;
            }

            //ERROR CASE

            _ = _logService.Log(username, _dbErrors.DBErrorCheck((int)res.Result) + " DeleteImage ", "DataStore", "Error");



            return res;
        }

        




    }
}
