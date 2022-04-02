using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL
{
    public interface IRepositoryMedication<T>
    {
        public int getFavoriteCount(string username);
        public bool addFavorite(string username, DrugName drug);
        public List<FavoriteDrug> ViewFavorites(string username);
        public int UpdateFavorite(string username, FavoriteDrug drug);
        public int RemoveFavorite(string drugProductID, string username);
    }
}
