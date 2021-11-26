using System;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class UserManagementService
    {

        UserManagementDAL UMD = new UserManagementDAL();

        public bool CreateAccount(User user) 
        {
            UMD.CreateAccount(user); 
            return true;
        }

    }
}
