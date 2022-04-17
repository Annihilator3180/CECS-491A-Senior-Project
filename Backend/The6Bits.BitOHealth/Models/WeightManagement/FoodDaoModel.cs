using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{


    public class FoodDaoModel
    {







        public string Username { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public float Calories { get; set; }

        public DateTime FoodLogDate { get; set; }
        public float? Carbs { get; set; }
        public float? Protein { get; set; }
        public float? Fat { get; set; }

        public FoodDaoModel(string username, string foodName, string description, float calories, DateTime foodLogDate, float? carbs = null, float? protein = null, float? fat = null)
        {
            Username = username;
            FoodName = foodName;
            Description = description;
            Calories = calories;
            FoodLogDate = foodLogDate;
            Carbs = carbs;
            Protein = protein;
            Fat = fat;
        }












    }
}
