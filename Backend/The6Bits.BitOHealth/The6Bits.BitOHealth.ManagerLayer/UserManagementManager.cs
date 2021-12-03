using System;
using System.ComponentModel.Design;
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
            if (UMS.ValidatePassword(user.Password))
            {
                return "Invalid Password";
            }
            string validation = UMS.ValidateUsername(user.Username);
            return validation != "new username" ? validation : UMS.CreateAccount(user);
        }

        public string DeleteAccount(string username)
        {
            UserManagementService UMS = new UserManagementService();
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            return UMS.DeleteAccount(username) ;
            

        }

        public string EnableAccount(string username)
        {
            UserManagementService UMS = new UserManagementService();
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            return UMS.EnableAccount(username);
        }

        public string UpdateAccount(string username)
        {
            UserManagementService UMS = new UserManagementService();
            string validation = UMS.ValidateUsername(username);
            if (validation != "username exists")
            {
                return validation;
            }
            return UMS.DeleteAccount(username);


        }

    }
}