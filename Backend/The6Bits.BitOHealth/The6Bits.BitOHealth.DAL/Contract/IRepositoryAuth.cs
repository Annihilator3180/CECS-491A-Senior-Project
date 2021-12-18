using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IRepositoryAuth<T>
    {
        string UsernameExists(string username);
        string UserRole(string username);
        string CheckPassword(string username,string password);

    }
}
