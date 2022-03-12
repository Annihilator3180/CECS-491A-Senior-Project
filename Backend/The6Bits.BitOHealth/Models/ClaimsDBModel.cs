using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class ClaimsDBModel 
    {
        public string Username { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

    }
}
