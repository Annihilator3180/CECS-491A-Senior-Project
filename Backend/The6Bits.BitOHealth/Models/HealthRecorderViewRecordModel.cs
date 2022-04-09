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
        private HttpResponseMessage httpResponse;
        private List<string> records = new List<string>();

        public HealthRecorderViewRecordModel(List<string> records)
        {
            this.records = records;
        }
        public HealthRecorderViewRecordModel(HttpResponseMessage response)
        {
            httpResponse = response;
        }

        public void SetHttpResponse(HttpResponseMessage response)
        {
            httpResponse = response;
        }
        public string ToString()
        {
            return JsonConvert.SerializeObject(this.records);

        }
    }
}
