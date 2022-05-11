using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class DietR
    {
        
        public string Diet { get; set; }
        public string Health { get; set; }
        public double Ingr { get; set; }
        public string DishType { get; set; }
        public double Calories { get; set; }
        public string CuisineType { get; set; }
        public string Excluded { get; set; }
        public string Q { get; set; }
        public string MealType { get; set; }
        public DietR()
        {
            Diet = "";
            Health = "";
            Ingr = 0.0;
            DishType = "";
            Calories = 0.0;
            CuisineType = "";
            Excluded = "";
            Q = "";
            MealType = "";

        }

        public DietR(string q, string diet, string health, double ingr, string dishType, double calories, string cuisineType, string excluded, string mealType)
        {
            Q = q;
            Diet = diet;
            Health = health;
            Ingr = ingr;
            DishType = dishType;
            Calories = calories;
            CuisineType = cuisineType;
            Excluded = excluded;
            MealType = mealType;

        }
    }


    public class Ingredient
    {
        public string text { get; set; }
        public double quantity { get; set; }
        public string measure { get; set; }
        public string food { get; set; }
        public double weight { get; set; }
        public string foodId { get; set; }
    }
    public class Ingredients
    {
        public String[] ingr { get; set; }
        public Ingredients()
        {
            ingr = new String[0];   
        }
        public Ingredients(String[] ing)
        {
            ingr = ing;
        }
    }
    public class Recipe
    {

        public List<string> ingredientLines { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public double calories { get; set; }
        public double totalWeight { get; set; }
        public List<string> cuisineType { get; set; }
        public List<string> mealType { get; set; }
        public List<string> dishType { get; set; }
        public List<string> healthLabels { get; set; }
        public object image { get; set; }
        public object url { get; set; }
        public string uri { get; set; }
        public object yield { get; set; }
        public object label { get; set; }

    }

    public class Hit
    {
        public Recipe recipe { get; set; }
        //public Links _links { get; set; }
    }

    public class EdamamResponseRoot
    {
        public double from { get; set; }
        public double to { get; set; }
        public double count { get; set; }
        //public Links _links { get; set; }
        public List<Hit> hits { get; set; }
    }


}