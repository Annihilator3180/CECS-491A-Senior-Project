using System;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;




namespace The6Bits.BitOHealth.ControllerLayer
{

    public class UMController 
    {

        public string CreateAccount(User user)
        {
            UMManager UMM = new UMManager();
            return UMM.CreateAccount(user);
        }
        public string DeleteAccount(string username)
        {
            UMManager UMM = new UMManager();
            return UMM.DeleteAccount(username);
        }
        public string UpdateAccount(User user)
        {
            UMManager UMM = new UMManager();
            return UMM.UpdateAccount(user);
        }
        public string EnableAccount(string username)
        {
            UMManager UMM = new UMManager();
            return UMM.EnableAccount(username);
        }
        public string DisableAccount(string username)
        {
            UMManager UMM = new UMManager();
            return UMM.DisableAccount(username);
        }

    }
}