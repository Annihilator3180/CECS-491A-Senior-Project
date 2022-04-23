using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class drugInfoResponse
    {
        public drugInfo data { get; set; }
        public string Error { get; set; }
        public bool isSuccess { get; set; }

        public drugInfoResponse(drugInfo data, string error, bool isSuccess)
        {
            this.data = data;
            Error = error;
            this.isSuccess = isSuccess;
        }
        public drugInfoResponse()
        {
            data = new drugInfo();
            Error = "";
            isSuccess = false;

        }
    }
}

