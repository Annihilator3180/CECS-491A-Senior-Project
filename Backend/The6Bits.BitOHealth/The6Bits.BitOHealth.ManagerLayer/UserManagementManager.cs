using System;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class UserManagementManager
    {
        UserManagementService UMS = new UserManagementService();

        public bool CreateAccount(User user)
        {
            UMS.CreateAccount(user);
            return true;
        }

    }
}