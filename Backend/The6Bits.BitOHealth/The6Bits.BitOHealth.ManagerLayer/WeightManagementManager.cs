using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class WeightManagementManager
    {


        private IRepositoryWeightManagementDao _dao;
        private WeightManagementService _WMS;
        public WeightManagementManager(IRepositoryWeightManagementDao dao)
        {
            _dao = dao;
            _WMS = new WeightManagementService(dao);
        }

        public string CreateGoal(int goalNum, string username)
        {
            string del = _WMS.DeleteGoal(username);
            if (del.Contains("Database"))
            {
                return "Error Deleting Goal" + del;
            }
            string res = _WMS.CreateGoal(goalNum,username);
            if (res.Contains("Database"))
            {
                return "Error Creating Goal" + res;
            }
            return res;

        }

    }
}
