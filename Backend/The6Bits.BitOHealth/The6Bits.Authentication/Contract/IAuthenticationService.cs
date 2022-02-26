using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.Authentication.Contract
{
    public interface IAuthenticationService
    {
        public string generateToken(string data);
        public string Decrypt(string encryptedData);
        public bool ValidateToken(string token);
        public string getUsername(string token);

    }
}
