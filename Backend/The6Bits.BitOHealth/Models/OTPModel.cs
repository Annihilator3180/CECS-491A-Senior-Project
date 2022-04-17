


using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class OTPModel
    {
        // optional for future use for updating users
        
        public string username { get; set; }
        public DateTime CodeDate { get; set; }
        public string code { get; set; }
        public string codeType { get; set; }

        public OTPModel()
        {
        }

        public OTPModel(string Username, DateTime CodeDate, string code, string codeType )
        {

            username = Username;
            CodeDate = CodeDate;
            code = code;
            codeType = codeType;

        }

    }
}