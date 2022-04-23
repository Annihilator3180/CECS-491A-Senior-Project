namespace The6Bits.BitOHealth.Models;

public class FoodModel
{
    public string? Username { get; set; }
    public string FoodName { get; set; }
    public string Description { get; set; }
    public double Calories { get; set; }

    public DateTime FoodLogDate { get; set; }
    public double? Carbs { get; set; }
    public double? Protein { get; set; }
    public double? Fat { get; set; }

    public int? Id { get; set; }

    public FoodModel(string foodName, string description, double calories, DateTime foodLogDate, double? carbs = null, double? protein = null, double? fat = null, int? id = null)
    {
        Id = id;
        FoodName = foodName;
        Description = description;
        Calories = calories;
        FoodLogDate = foodLogDate;
        Carbs = carbs;
        Protein = protein;
        Fat = fat;
    }



}