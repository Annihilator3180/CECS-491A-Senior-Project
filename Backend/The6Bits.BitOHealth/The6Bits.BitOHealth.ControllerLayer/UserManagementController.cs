using System;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;


namespace The6Bits.BitOHealth.ControllerLayer
{
    public class UserManagementController
    {

        UserManagementManager UMM = new UserManagementManager();



        public bool CreateAccount(User user)
        {
            UMM.CreateAccount(user);
            return true;
        }

    }
}