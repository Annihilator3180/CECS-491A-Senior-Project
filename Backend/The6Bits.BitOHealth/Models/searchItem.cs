using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class searchItem
    {
        public string itemSearched { get; set; }
        public string occurences { get; set; }

        public searchItem()
        {
            itemSearched = "";
            occurences = "";
        }
    }
}
