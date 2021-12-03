using System;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;




namespace The6Bits.BitOHealth.ControllerLayer
{

    public class UserManagementController 
    {

        UserManagementManager UMM = new UserManagementManager();

        public string CreateAccount(User user)
        {
            return UMM.CreateAccount(user);
        }
        public string DeleteAccount(string username)
        {
            return UMM.DeleteAccount(username);
        }
        public string UpdateAccount(string username)
        {
            return UMM.UpdateAccount(username);
        }
        public string EnableAccount(string username)
        {
            return UMM.EnableAccount(username);
        }
        

    }
}