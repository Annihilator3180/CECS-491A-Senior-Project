using System;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class UserManagementService
    {

        UserManagementDAL<User> UMD = new UserManagementDAL<User>();

        public bool CreateAccount(User user) 
        {
            UMD.Create(user); 
            return true;
        }

    }
}
