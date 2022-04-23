using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class FavoriteDrug
    {
        public string generic_name { get; set; }
        public string product_ndc { get; set; }
        public string brand_name { get; set; }
        
        public double lowestprice { get; set; }
        public string lowestPriceLocation { get; set; } 
        public string description { get; set; }
        public FavoriteDrug()
        {
            generic_name = "";
            product_ndc = "";
            brand_name = "";
            lowestprice = 0;
            lowestPriceLocation = "";
            description = "";
        }
        public FavoriteDrug( string genericName, string productID, string brandName, int lowestPrice, string lowestpriceLocation,string describe)
        {
            generic_name = genericName;
            product_ndc = productID;
            brand_name = brandName;
            lowestprice = lowestPrice;
            lowestPriceLocation = lowestpriceLocation;
            description = describe;
        }
    }
}
