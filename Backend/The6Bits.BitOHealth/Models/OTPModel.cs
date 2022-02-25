


using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class OTPModel
    {
        // optional for future use for updating users
        
        public string username { get; set; }
        public DateTime time { get; set; }
        public string code { get; set; }
        public string codeType { get; set; }

        public OTPModel()
        {
        }

        public OTPModel(string username, DateTime time, string code, string codeType )
        {

            username = username;
            time = time;
            code = code;
            codeType = codeType;

        }

    }
}