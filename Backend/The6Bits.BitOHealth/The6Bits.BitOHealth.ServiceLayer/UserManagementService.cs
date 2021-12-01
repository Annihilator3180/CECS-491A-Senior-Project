using System;
using System.ComponentModel.DataAnnotations;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class UserManagementService
    {

        UserManagementDAL<User> UMD = new UserManagementDAL<User>();


        public bool ValidateEmail(string email)
        {
            try
            {
                return new EmailAddressAttribute().IsValid(email);
            }
            catch
            {
                return false;
            }
        }

        //TODO: DOUBLE CHECK VALIDATIONS IN BRD
        public bool ValidatePassword(string password)
        {
            try
            {
                return password.Any(char.IsUpper) & password.Any(char.IsLower) & password.Any(char.IsDigit) & password.Length > 8;
            }
            catch
            {
                return false;
            }
        }

        //TODO: DOUBLE CHECK VALIDATIONS IN BRD
        // catch db error
        public string ValidateUsername(string username)
        {
            if (!username.Any(char.IsAscii) & username.Length < 15)
            {
                return "Invalid Username";
            }
            else if(UMD.Read(username) != null)
            {
                return "Username Exists ";
            }
            return "new username";
        }


        public string CreateAccount(User user) 
        {
            return UMD.Create(user);
        }

    }
}
