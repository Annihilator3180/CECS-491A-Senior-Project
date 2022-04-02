using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class DrugInfo
    {
        public string spl_medguide { get; set; }
        public string adverse_reactions { get; set; }
        public string dosage_forms_and_strengths { get; set; }  
        public string indications_and_usage { get; set; }
        public FavoriteDrug favoriteDrug { get; set; }
        public DrugInfo(string splmedguide, string adversereactions, string dosageformsandstrengths, string indicationsandusage)
        {
            spl_medguide= splmedguide;
            adverse_reactions= adversereactions;
            dosage_forms_and_strengths = dosageformsandstrengths;
            indications_and_usage=indicationsandusage;


        }
    }
}
