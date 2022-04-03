using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Contracts
{
    public class INewsAPI<T>
    {
        public Task<IEnumerable<T>> QueryNews(string queryString);
    }
}
