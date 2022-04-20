﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class HealthRecorderRequestModel
    {
        public string RecordName { get; set; }
        public string CategoryName { get; set; }
        public string? RecordNumber { get; set; }

        public HealthRecorderRequestModel(string recordName, string categoryName, string recordNumber)
        {
            RecordName = recordName;
            CategoryName = categoryName;
            RecordNumber = recordNumber;

        }
        public HealthRecorderRequestModel(string recordName, string categoryName)
        {
            RecordName = recordName;
            CategoryName = categoryName;
        }
        public HealthRecorderRequestModel()
        {

        }
       
    }
}
