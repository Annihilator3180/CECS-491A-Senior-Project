using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class Tracking
    {
        public int count { get; set; }
        public DateTime dateTime { get; set; }
        public string logType { get; set; }
        public string date { get; set; }
        public Tracking()
        {
            count = 0;
            dateTime = new DateTime();
            logType = "";
            date = "";
        }
        public Tracking(DateTime datetime)
        {
            count = 0;
            dateTime = datetime;
            logType = "";
            date= datetime.ToString("MM/dd/yyyy");
        }
    }
   
}
