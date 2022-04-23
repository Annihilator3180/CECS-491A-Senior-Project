


using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class DrugName
    {

        public string generic_name { get; set; }
        public string product_ndc { get; set; }
        public string brand_name { get; set; }

        public DrugName()
        {
            generic_name ="";
            product_ndc = "";
            brand_name = "";
        }

        public DrugName(string genericName, string productNdc, string brandName)
        {
            generic_name = genericName;
            product_ndc = productNdc;
            brand_name = brandName;
        }
    }
}