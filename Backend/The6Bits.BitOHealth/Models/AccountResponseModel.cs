using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class AccountResponseModel
    {
         
        string data;
        string errorMessage;

        public AccountResponseModel()
        {
            data = null;
            errorMessage = null;
        }
        public string Data { get; set; }
        public string ErrorMessage { get; set; }

    }
}

