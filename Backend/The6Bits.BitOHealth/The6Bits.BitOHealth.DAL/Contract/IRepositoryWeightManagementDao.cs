using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL
{
    public interface IRepositoryWeightManagementDao
    {
        public string Delete(string username);
        public string Create(GoalWeightModel goal, string username);
        public GoalWeightModel Read(string username);

        public string Update(GoalWeightModel goal, string username);


        public string CreateFoodLog(FoodModel food, string username);
    }
}
