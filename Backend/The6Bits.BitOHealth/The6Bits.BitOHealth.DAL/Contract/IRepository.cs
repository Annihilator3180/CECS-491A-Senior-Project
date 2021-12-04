using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL
{
    public interface IRepository<T>
    {
        string Create(T model);
        T Read(T model);
        bool Update(T model);
        bool Delete(T model);

        bool EnableAccount(string username);
        bool DisableAccount(string username);

    }
}
