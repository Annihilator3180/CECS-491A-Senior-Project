using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class GoalWeightModel
    {
        public int GoalWeight { get; set; }

        public DateTime GoalDate { get; set; }

        public string ExerciseLevel { get; set; }

        public GoalWeightModel(string username, int goalWeight, DateTime goalDate, string exerciseLevel)
        {
            GoalWeight = goalWeight;
            GoalDate = goalDate;
            ExerciseLevel = exerciseLevel;
        }
    }
}
