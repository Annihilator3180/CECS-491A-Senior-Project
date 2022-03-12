using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ManagerLayer
{
    public class WeightManagementManager
    {


        private IRepositoryWeightManagementDao _dao;
        private WeightManagementService _WMS;
        private IDBErrors _dbErrors;
        public WeightManagementManager(IRepositoryWeightManagementDao dao, IDBErrors dbErrors)
        {
            _dao = dao;
            _WMS = new WeightManagementService(dao);
            _dbErrors = dbErrors;
        }

        public string CreateGoal(int goalNum, string username)
        {
            string del = _WMS.DeleteGoal(username);
            if (!del.Contains("Weight"))
            {
                return "Error Deleting Goal" + _dbErrors.DBErrorCheck(Int32.Parse(del));
            }
            string res = _WMS.CreateGoal(goalNum,username);
            if (!res.Contains("Weight"))
            {
                return "Error Creating Goal" + _dbErrors.DBErrorCheck(Int32.Parse(res));
            }
            return res;

        }

    }
}
