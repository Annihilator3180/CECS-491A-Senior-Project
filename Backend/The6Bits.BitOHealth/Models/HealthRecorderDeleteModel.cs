using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class HealthRecorderDeleteModel
    {
        public string RecordName { get; set; }
        public string CategoryName { get; set; }

        public HealthRecorderDeleteModel(string recordName, string categoryName)
        {
            RecordName = recordName;
            CategoryName = categoryName;
        }
        public HealthRecorderDeleteModel()
        {

        }
       
    }
}
