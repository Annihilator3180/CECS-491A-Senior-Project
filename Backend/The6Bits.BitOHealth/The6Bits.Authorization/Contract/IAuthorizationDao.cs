using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.Authorization.Contract
{
    public interface IAuthorizationDao
    {

        public ClaimsIdentity getClaims(string data);
        //public string VerifyClaims(string token, string claim);

    }
}
