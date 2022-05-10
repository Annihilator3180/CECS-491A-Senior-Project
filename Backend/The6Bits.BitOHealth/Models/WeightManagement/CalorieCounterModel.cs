namespace The6Bits.BitOHealth.Models;

public class CalorieCounterModel
{
    public CalorieCounterModel(int calorieRecommendation, int weekCaloriesEaten, int todayCaloriesEaten, int caloriesCompared, int dailyAverageThisWeekCalories)
    {
        CalorieRecommendation = calorieRecommendation;
        WeekCaloriesEaten = weekCaloriesEaten;
        TodayCaloriesEaten = todayCaloriesEaten;
        CaloriesCompared = caloriesCompared;
        DailyAverageThisWeekCalories = dailyAverageThisWeekCalories;
    }

    public CalorieCounterModel()
    {
        
    }

    public int CalorieRecommendation { get; set; }

    public int WeekCaloriesEaten { get; set; }
    
    public int TodayCaloriesEaten { get; set; }

    public int CaloriesCompared { get; set; }
    
    public int DailyAverageThisWeekCalories { get; set; }


}