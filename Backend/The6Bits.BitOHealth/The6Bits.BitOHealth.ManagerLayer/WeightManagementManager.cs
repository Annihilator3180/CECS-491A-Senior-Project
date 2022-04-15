using System;
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


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class WeightManagementManager
    {


        private IRepositoryWeightManagementDao _dao;
        private WeightManagementService _WMS;
        private IDBErrors _dbErrors;
        private readonly IFoodAPI<Parsed> _foodAPI;


        public WeightManagementManager(IRepositoryWeightManagementDao dao, IDBErrors dbErrors, IFoodAPI<Parsed> foodApiService)
        {
            _dao = dao;
            _WMS = new WeightManagementService(dao);
            _dbErrors = dbErrors;
            _foodAPI = foodApiService;
        }

        public string CreateGoal(GoalWeightModel goal, string username)
        {
            string del = _WMS.DeleteGoal(username);
            if (!del.Contains("Weight"))
            {
                return "Error Deleting Goal" + _dbErrors.DBErrorCheck(Int32.Parse(del));
            }
            string res = _WMS.CreateGoal(goal,username);
            if (!res.Contains("Weight"))
            {
                return "Error Creating Goal" + _dbErrors.DBErrorCheck(Int32.Parse(res));
            }
            return res;

        }



        public async Task<IEnumerable<Parsed>> SearchFood(string queryString)
        {
            return await _foodAPI.QueryFoods(queryString);
        }


        public async Task<string> UpdateGoal(GoalWeightModel goal,string username)
        {
            return await _WMS.UpdateGoal(goal, username);
        }


        //TODO:ASK ABOUT THIS
        public async Task<GoalWeightModel> ReadGoal(string username)
        {
            return await _WMS.ReadGoal( username);
        }


        public async Task<string> StoreFoodLog(FoodModel food,string username)
        {
            return await _WMS.StoreFoodLog(food, username);
        }


    }
}
