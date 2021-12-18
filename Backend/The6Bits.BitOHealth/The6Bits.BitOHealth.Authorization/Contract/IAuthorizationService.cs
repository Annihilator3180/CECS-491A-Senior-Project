using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Authorization.Contract
{
    public interface IAuthorizationService
    {

        public string generateToken(string data);
        public string Decrypt(string encryptedData);
        public bool VerifyClaims(string token, string access);

    }
}
