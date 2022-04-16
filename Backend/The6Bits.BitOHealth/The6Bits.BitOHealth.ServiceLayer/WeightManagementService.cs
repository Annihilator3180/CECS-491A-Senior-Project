using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class WeightManagementService
    {





        private IRepositoryWeightManagementDao _WMD;
        
        public WeightManagementService(IRepositoryWeightManagementDao dao)
        {
            _WMD = dao;
        }

        public string DeleteGoal(string username)
        {
            return _WMD.Delete(username);
        }


        public string CreateGoal(GoalWeightModel goal, string username)
        {

            return _WMD.Create(goal,username);

        }

        public async Task<string> UpdateGoal(GoalWeightModel goal, string username)
        {
            return _WMD.Update(goal, username);
        }

        public async Task<GoalWeightModel> ReadGoal( string username)
        {
            return _WMD.Read(username);
        }



        //TODO:ADD TRY CATCH RETURN OBJECT?
        public async Task<string> StoreFoodLog(FoodModel food, string username)
        {
            return _WMD.CreateFoodLog(food, username);
        }

        public async Task<IEnumerable<FoodModel>> GetFoodLogs(string username)
        {
            return _WMD.GetFoodLogs(username);
        }



    }
}
