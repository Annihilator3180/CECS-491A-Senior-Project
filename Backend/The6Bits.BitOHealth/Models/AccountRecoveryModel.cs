using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class AccountRecoveryModel
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public AccountRecoveryModel(string username, string email)
        {
            this.Username = username;
            this.Email = email;
        }


    }
}