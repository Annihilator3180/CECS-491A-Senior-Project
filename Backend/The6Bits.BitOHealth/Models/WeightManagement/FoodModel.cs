namespace The6Bits.BitOHealth.Models;

public class FoodModel
{
    public string FoodName { get; set; }
    public string Description { get; set; }
    public int Calories { get; set; }
    public int? Carbs { get; set; }
    public int? Protein { get; set; }
    public int? Fat { get; set; }

    public FoodModel(string foodName, string description, int calories, int? carbs = null, int? protein = null, int?fat = null)
    {
        FoodName = foodName;
        Description = description;
        Calories = calories;
        Carbs = carbs;
        Protein = protein;
        Fat = fat;
    }

}