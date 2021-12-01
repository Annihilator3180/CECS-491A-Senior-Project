using System;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.BitOHealth.Models;


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class UserManagementManager
    {

        public string CreateAccount(User user)
        {
            UserManagementService UMS = new UserManagementService();
            if (!UMS.ValidateEmail(user.Email))
            {
                return "Invalid Email";
            }
            else if (UMS.ValidatePassword(user.Password))
            {
                return "Invalid Password";
            }
            else if (UMS.ValidateUsername(user.Username) != "new username")
            {
                return UMS.ValidateUsername(user.Username);
            }
            return UMS.CreateAccount(user);
        }

    }
}