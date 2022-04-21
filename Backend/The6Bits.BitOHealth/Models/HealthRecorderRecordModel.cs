using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class HealthRecorderRecordModel
    {
        string record1;
        DateTime timeSaved;
        string username;
        string categoryName;
        string recordName;
        string errorCode;
        string record2;

        public HealthRecorderRecordModel()
        {
        }

        public HealthRecorderRecordModel(string record1, string record2, string username, string categoryName, string recordName)
        {
            this.record1 = record1;
            this.record2 = record2;
            this.username = username;
            this.categoryName = categoryName;
            this.recordName = recordName;
        }
       

        public string Record1 { get; set; }
        public string Username { get; set; }
        public string CategoryName { get; set; }
        public string RecordName { get; set; }
        public string Record2 { get; set; }
        public string ErrorCode { get; set; }




    }
}
