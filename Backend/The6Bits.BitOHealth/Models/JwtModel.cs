using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class JwtPayloadModel
    {
        public string username { get; set; }
        public string iat { get; set; }
        public ClaimsIdentity Claims { get; set; }
    }
}
