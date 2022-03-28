using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FoodAPI.Contracts
{
    public interface IFoodAPI<T> 
    {
        public Task<IEnumerable<T>> QueryFoods(string queryString);

    }
}
