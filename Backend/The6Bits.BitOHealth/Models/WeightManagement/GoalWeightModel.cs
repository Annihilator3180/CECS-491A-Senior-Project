using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class GoalWeightModel
    {
        public int? GoalWeight { get; set; }
        public DateTime? GoalDate { get; set; }
        public int ExerciseLevel { get; set; }
        public int CurrentWeight { get; set; }


        public GoalWeightModel( int goalWeight, DateTime goalDate, int exerciseLevel, int currentWeight)
        {
            GoalWeight = goalWeight;
            GoalDate = goalDate;
            ExerciseLevel = exerciseLevel;
            CurrentWeight = currentWeight;
        }
        public GoalWeightModel()
        {

        }
    }
}
