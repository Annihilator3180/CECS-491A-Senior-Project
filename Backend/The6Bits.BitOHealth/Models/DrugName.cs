


using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class DrugName
    {

        public string generic_name { get; set; }
        public string product_id { get; set; }
        public string brand_name { get; set; }
        public int isFavorite { get; set; }

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
            isFavorite = 0;
        }
        public DrugName(string genericName, string productID, string brandName, int is_Favorite)
        {
            generic_name = genericName;
            product_id = productID;
            brand_name = brandName;
            isFavorite = is_Favorite;
        }

    }
}