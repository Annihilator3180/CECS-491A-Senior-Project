namespace The6Bits.BitOHealth.Models;

public class FoodModel
{
    public string FoodName { get; set; }
    public string Description { get; set; }
    public float Calories { get; set; }

    public DateTime FoodLogDate { get; set; }
    public float? Carbs { get; set; }
    public float? Protein { get; set; }
    public float? Fat { get; set; }

    public FoodModel(string foodName, string description, float calories, DateTime foodLogDate, float? carbs = null, float? protein = null, float? fat = null)
    {
        FoodName = foodName;
        Description = description;
        Calories = calories;
        FoodLogDate = foodLogDate;
        Carbs = carbs;
        Protein = protein;
        Fat = fat;
    }

}