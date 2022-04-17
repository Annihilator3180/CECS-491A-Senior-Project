using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public  class FindDrugResponse
    {
        public List<DrugName> data { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
        public FindDrugResponse()
        {
            data = new List<DrugName>();
            success = false;
            error = "";
        }
    }
}
