using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL;

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


        public string CreateGoal(int goalNum, string username)
        {

            return _WMD.Create(goalNum,username);

        }





    }
}
