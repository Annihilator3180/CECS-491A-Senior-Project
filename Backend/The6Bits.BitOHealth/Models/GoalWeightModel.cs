using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class GoalWeightModel
    {
        public string Username { get; set; }
        public int Goal { get; set; }

        public DateTime GoalDate { get; set; }

        public string PictureName { get; set; }

        public GoalWeightModel(string username, int goal, DateTime goalDate, string pictureName)
        {
            Username = username;
            Goal = goal;
            GoalDate = goalDate;
            PictureName = pictureName;
        }
    }
}
