using System;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;




namespace The6Bits.BitOHealth.ControllerLayer
{

    public class UserManagementController 
    {



        public string CreateAccount(User user)
        {
            UserManagementManager UMM = new UserManagementManager();
            return UMM.CreateAccount(user);
        }

    }
}