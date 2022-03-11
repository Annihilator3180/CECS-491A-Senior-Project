using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace DietRecommendation.Contract
{
    public interface IDietRecommendations
    {
        public string SaveDietResponses(DietR d) ;

    }
}
