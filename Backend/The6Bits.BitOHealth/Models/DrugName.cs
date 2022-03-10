


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

        public DrugName(string generic_name, string product_id, string brand_name)
        {
            generic_name = generic_name;
            product_id = product_id;
            brand_name = brand_name;

        }

    }
}