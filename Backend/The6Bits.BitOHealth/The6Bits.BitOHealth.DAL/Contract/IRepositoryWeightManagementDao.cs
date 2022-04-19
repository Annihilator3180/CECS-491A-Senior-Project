using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL
{
    public interface IRepositoryWeightManagementDao<T>
    {
        public Task<T> Delete(string username);
        public Task<T> Create(GoalWeightModel goal, string username);
        public Task<T> Read(string username);

        public Task<T> Update(GoalWeightModel goal, string username);


        public Task<T> CreateFoodLog(FoodModel food, string username);

        public Task<T> GetFoodLogs(string username);

        public Task<T> GetFoodLogsAfter(DateTime dateTime, string username);

        public Task<T> DeleteFoodLog(int id, string username);


    }
}
