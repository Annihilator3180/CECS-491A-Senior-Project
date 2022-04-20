using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IRepositoryDietRecommendations
    {
        public string SaveDietResponses(DietR d, string username);
        public Task<String> AddToFavorite(FavoriteRecipe recipe, string username);
        public Task<string> DeleteFavorite ( string recipeid );
        public Task<List<string>> GetFavorites(string username);

    }
}
