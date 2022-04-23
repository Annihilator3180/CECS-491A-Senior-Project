using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class HealthRecorderExportModel
    {

        private string? file;
        private string? errorMessage;

        public string? File { get; set; }


        public string? ErrorMessage { get; set; }
       
    }
}

