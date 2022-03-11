using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class DietR
    {
        public string Diet { get; set; }
        public string Health { get; set; }
        public string Ingr { get; set; }
        public string DishType { get; set; }
        public string Calories { get; set; }
        public string Time { get; set; }
        public string Excluded { get; set; }
        public string Q { get; set; } 

        public DietR()
        { }

        public DietR(string diet, string health, string ingr, string dishType, string calories, string time, string excluded)
        {
            Diet = diet;
            Health = health;
            Ingr = ingr;    
            DishType = dishType;    
            Calories = calories;    
            Time = time;
            Excluded = excluded;

        }
    }

    public class Self
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Next
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Next next { get; set; }
    }

    public class THUMBNAIL
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class SMALL
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class REGULAR
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class LARGE
    {
        public string url { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class Images
    {
        public THUMBNAIL THUMBNAIL { get; set; }
        public SMALL SMALL { get; set; }
        public REGULAR REGULAR { get; set; }
        public LARGE LARGE { get; set; }
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

    public class TotalNutrients
    {
    }

    public class TotalDaily
    {
    }


    public class Digest
    {
        public string label { get; set; }
        public string tag { get; set; }
        public string schemaOrgTag { get; set; }
        public double total { get; set; }
        public bool hasRDI { get; set; }
        public double daily { get; set; }
        public string unit { get; set; }
    }

    public class Recipe
    {
        public string uri { get; set; }
        public string label { get; set; }
        public string image { get; set; }
        public Images images { get; set; }
        public string source { get; set; }
        public string url { get; set; }
        public string shareAs { get; set; }
        //public double? yield { get; set; }
        public List<string> dietLabels { get; set; }
        public List<string> healthLabels { get; set; }
        public List<string> cautions { get; set; }
        public List<string> ingredientLines { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public double calories { get; set; }
        public double glycemicIndex { get; set; }
        public double totalCO2Emissions { get; set; }
        public string co2EmissionsClass { get; set; }
        public double totalWeight { get; set; }
        public List<string> cuisineType { get; set; }
        public List<string> mealType { get; set; }
        public List<string> dishType { get; set; }
        public TotalNutrients totalNutrients { get; set; }
        public TotalDaily totalDaily { get; set; }
        public List<Digest> digest { get; set; }
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
