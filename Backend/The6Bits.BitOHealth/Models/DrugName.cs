


using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class DrugName
    {
        // optional for future use for updating users

        public string generic_name { get; set; }
        public string product_id { get; set; }
        public string brand_name { get; set; }

        public DrugName()
        {
            generic_name ="";
            product_id = "";
            brand_name = "";
        }

        public DrugName(string genericName, string productID, string brandName)
        {
            generic_name = genericName;
            product_id = productID;
            brand_name = brandName;

        }

    }
}