using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IRepositoryNutritionAnalysis
    {
        public string SaveRecipeResponse(string username, Ingredients ingredients);

    }
}


