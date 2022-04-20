using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class FavoriteRecipe
    {
        public string Recipe_id { get; set; }  
        public FavoriteRecipe()
        {
            Recipe_id = "";

        }
        public FavoriteRecipe(string recipe_id)
        {
           Recipe_id = recipe_id;
        }

    }

}
