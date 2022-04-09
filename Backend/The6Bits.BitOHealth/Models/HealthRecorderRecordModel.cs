using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class HealthRecorderRecordModel
    {
        string record;
        DateTime timeSaved;
        string username;
        string categoryName;
        string recordName;
        string errorCode;
        string secondRecord;

        public HealthRecorderRecordModel(string record, string secondRecord, DateTime timeSaved, string username, string categoryName, string recordName)
        {
            this.record = record;
            this.secondRecord = secondRecord;
            this.timeSaved = timeSaved;
            this.username = username;
            this.categoryName = categoryName;
            this.recordName = recordName;
        }
        public HealthRecorderRecordModel()
        {

        }

        public string Record { get; set; }
        public DateTime TimeSaved { get; set; }
        public string Username { get; set; }
        public string CategoryName { get; set; }
        public string RecordName { get; set; }
        public string SecondRecord { get; set; }
        public string ErrorCode { get; set; }




    }
}
