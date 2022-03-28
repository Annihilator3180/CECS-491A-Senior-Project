using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MapAPI.Contracts
{
    public interface IMapAPI<T>
    {
        public Task<IEnumerable<T>> QueryLocations(string queryString);

    }
}
