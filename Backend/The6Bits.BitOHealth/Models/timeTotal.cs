using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class timeTotal
    {
        public string viewName { get; set; }
        public float seconds { get; set; }
        public int occurences { get; set; }

        public timeTotal()
        {
        }

        public timeTotal(string viewName, float seconds, int occurences)
        {

            this.viewName = viewName;
            this.seconds = seconds;
            this.occurences = occurences;
        }
    }
}
