using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;


namespace The6Bits.BitOHealth.ServiceLayer.Contract
{
    public interface IUMService
    {

        string CreateAccount(User user);
        string UpdateAccount(User user);
        string DeleteAccount(string username);
        string EnableAccount(string username);
        string DisableAccount(string username);
        bool ValidateEmail(string email);
        bool ValidatePassword(string password);
        string ValidateUsername(string username);



    }
}

