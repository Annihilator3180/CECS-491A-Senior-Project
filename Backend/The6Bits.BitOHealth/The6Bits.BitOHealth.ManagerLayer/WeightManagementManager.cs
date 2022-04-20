﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodAPI;
using FoodAPI.Contracts;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models.WeightManagement;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class WeightManagementManager
    {


        private IRepositoryWeightManagementDao<IWeightManagerResponse> _dao;
        private WeightManagementService _WMS;
        private IDBErrors _dbErrors;
        private readonly IFoodAPI<Parsed> _foodAPI;
        private ILogDal _logDal;


        public WeightManagementManager(IRepositoryWeightManagementDao<IWeightManagerResponse> dao, IDBErrors dbErrors, IFoodAPI<Parsed> foodApiService, ILogDal logDal, IRepositoryWeightManagementImageDao<IWeightManagerResponse> imageDao)
        {
            _dao = dao;
            _WMS = new WeightManagementService(dao, logDal, imageDao);
            _dbErrors = dbErrors;
            _foodAPI = foodApiService;
        }

        public async Task<IWeightManagerResponse> CreateGoal(GoalWeightModel goal, string username)
        {
            IWeightManagerResponse del = await _WMS.DeleteGoal(username);


            //ERROR CASE
            if (del.IsError is true )
            {
                return del;
            }


            IWeightManagerResponse create = await _WMS.CreateGoal(goal,username);

           
            return create;

        }



        public async Task<IWeightManagerResponse> SearchFood(string queryString)
        {
            if (queryString.Length is > 100 or 0)
            {
                return new WeightManagerResponse("Invalid Search", true, true);
            }

            return new WeightManagerResponse( await _foodAPI.QueryFoods(queryString));
        }


        public async Task<IWeightManagerResponse> UpdateGoal(GoalWeightModel goal,string username)
        {
            return await _WMS.UpdateGoal(goal, username);
        }


        public async Task<IWeightManagerResponse> ReadGoal(string username)
        {
            return await _WMS.ReadGoal( username);
        }


        public async Task<IWeightManagerResponse> StoreFoodLog(FoodModel food,string username)
        {

            //VALIDATION
            IWeightManagerResponse validation =
                await _WMS.GetFoodLogsAfterAddTime(DateTime.UtcNow.AddDays(-1), username);
            if (validation.IsError is true)
            {
                return validation;
            }

            List<FoodModel> foodLogs = ((IEnumerable<FoodModel>)validation.Result).ToList();

            if (foodLogs.Count > 20)
            {
                return new WeightManagerResponse("Too Many Food Logs done in one day. Try again tomorrow.", true, true);
            }


            //IF PASS VALIDATION



            return await _WMS.StoreFoodLog(food, username);
        }

        
        public async Task<IWeightManagerResponse> GetFoodLogs(string username)
        {
            return await _WMS.GetFoodLogs(username);
        }




        public async Task<IWeightManagerResponse> DeleteFoodLog(int id ,string username)
        {
            return await _WMS.DeleteFoodLog(id,username);
        }







        public async Task<IWeightManagerResponse> GetProfileInfo(string username)
        {
            try
            {

                CalorieCounterModel profileData = new CalorieCounterModel();
                double weekCal = 0;
                double dayCal = 0;
                double weekAvg = 0;





                IWeightManagerResponse getLogsResponse =
                    await _WMS.GetFoodLogsAfter(DateTime.UtcNow.AddDays(-7), username);


                if (getLogsResponse.IsError != null && getLogsResponse.IsError == true)
                {
                    return getLogsResponse;
                }

                IEnumerable<FoodModel> weekLogs = (IEnumerable<FoodModel>) getLogsResponse.Result;



                foreach (var log in weekLogs)
                {
                    weekCal += log.Calories;
                    if (log.FoodLogDate > DateTime.UtcNow.AddDays(-1))
                    {
                        dayCal += log.Calories;
                    }

                }

                weekAvg = weekCal / 7;
                profileData.DailyAverageThisWeekCalories = (int) weekAvg;
                profileData.WeekCaloriesEaten = (int) weekCal;
                profileData.TodayCaloriesEaten = (int) dayCal;

                IWeightManagerResponse readGoalResponse = await _WMS.ReadGoal(username);

                if (readGoalResponse.IsError != null && readGoalResponse.IsError == true)
                {
                    return readGoalResponse;
                }


                GoalWeightModel goalWeightModel = (GoalWeightModel) readGoalResponse.Result;


                if (goalWeightModel.GoalWeight < goalWeightModel.CurrentWeight)
                {
                    profileData.CalorieRecommendation = goalWeightModel.ExerciseLevel - 300;
                    profileData.CaloriesCompared = profileData.CalorieRecommendation - profileData.TodayCaloriesEaten;
                }
                else
                {
                    profileData.CalorieRecommendation = goalWeightModel.ExerciseLevel + 300;
                    profileData.CaloriesCompared = profileData.CalorieRecommendation - profileData.TodayCaloriesEaten;
                }

                return new WeightManagerResponse(profileData);

            }
            catch (Exception ex)
            {

                LogService logService = new LogService(_logDal);
                _ = logService.Log(username, "GetProfileInfo Internal Error", "Business", "Error");



                return new WeightManagerResponse("GetProfileInfo Internal Error" + ex.Message);
            }






        }
        
        
        
        
        public async Task<IWeightManagerResponse> SaveImage(IFormFile file,string username)
        {
            //VALIDATIONS
            string[] arrSplit = file.FileName.Split('.');
            if (arrSplit.Length < 2)
            {
                return new WeightManagerResponse("Invalid File Type.", true, true);
            }


            string fileType = arrSplit.Last();
            if (fileType is not ( "jpg" or "pdf"))
            {
                return new WeightManagerResponse("Invalid File Type.", true, true);
            }


            if (500000 > file.Length || file.Length > 1.6e+7)
            {
                return new WeightManagerResponse("Invalid File Size.",true, true);
            }




            IWeightManagerResponse saveImage = await _WMS.SaveImage(file, username);
            //ERROR CASE
            if (saveImage.IsError is true) return saveImage;



            string path = (string)saveImage.Result;


            IWeightManagerResponse saveImagePath = await _WMS.SaveImagePath(path, username);


            return saveImagePath;
        }
        public async Task<IWeightManagerResponse> DeleteImage(int imageId, string username )
        {

            IWeightManagerResponse read = await  _WMS.GetImage(imageId, username);
            //error case
            if (read.IsError is true) return read;



            IWeightManagerResponse del = await _WMS.DeleteImage((string)read.Result,username);
            //error case
            if (del.IsError is true) return del;





            return await _WMS.DeleteImagePath(imageId, username);
        }
        
        public async Task<IWeightManagerResponse> GetImage(int index, string username )
        {

            return await _WMS.GetImage(index, username);
        }

        public async Task<IWeightManagerResponse> GetAllImageIds(string username)
        {

            return await _WMS.GetAllImageIds( username);
        }

        public async Task<IWeightManagerResponse> DeleteGoal(string username)
        {

            return await _WMS.DeleteGoal( username);
        }


    }
}

