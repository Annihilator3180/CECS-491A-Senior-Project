namespace The6Bits.BitOHealth.Models;

public class FoodModel
{
    private string FoodName { get; set; }
    private string Description { get; set; }
    private int Calories { get; set; }
    private int? Carbs { get; set; }
    private int? Protein { get; set; }
    private int? Fat { get; set; }

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