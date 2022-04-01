﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class FavoriteDrug
    {
        public string username { get; set; }
        public string generic_name { get; set; }
        public string product_id { get; set; }
        public string brand_name { get; set; }
        
        public int lowestprice { get; set; }
        public string lowestPriceLocation { get; set; } 

        public FavoriteDrug()
        {
            username = "";
            generic_name = "";
            product_id = "";
            brand_name = "";
            lowestprice = 0;
            lowestPriceLocation = "";
        }
    }
}
