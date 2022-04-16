using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class HealthRecorderViewRecordModel
    //this model can consist of a list another "view" object with all things you need

    {
        private List<HealthRecorderRecordModel> records = new List<HealthRecorderRecordModel>();
        private string errorMessage;

        public List<HealthRecorderRecordModel>? Records { get; set; }


        public string? ErrorMessage { get; set; }
        public string ToString()
        {
            //still need to return 
            return JsonConvert.SerializeObject(this);

        }
    }
}
