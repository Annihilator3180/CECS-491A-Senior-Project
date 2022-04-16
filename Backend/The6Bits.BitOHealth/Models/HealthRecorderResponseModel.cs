using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class HealthRecorderResponseModel
    {
        string data;
        HttpResponseMessage httpResponse;
        string errorMessage;

        public HealthRecorderResponseModel()
        {
            data = null;
            errorMessage = null;
        }
        public string Data { get; set; }
        public string ErrorMessage { get; set; }
        public string ToString()
        {
            //still need to return 
            return JsonConvert.SerializeObject(this);

        }
    }
}
