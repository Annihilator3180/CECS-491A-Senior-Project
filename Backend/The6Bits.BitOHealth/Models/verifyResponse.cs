using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class verifyResponse
    {
        public int isSuccess { get; set; }
        public string data { get; set; }
        public string ErrorMessage { get; set; }
        public string username { get; set; }
        
        public verifyResponse()
        {
            isSuccess = 0;
            data = "";
            ErrorMessage = "";
            username = "";
        }
    }

}
