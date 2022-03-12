using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.HashAndSaltService;

    public interface IHashAndSalt
    {
        public string HashAndSalt(string password);
        public string HashAndSalt(string password, string salt);
        public string GetSalt(string username);

    }

