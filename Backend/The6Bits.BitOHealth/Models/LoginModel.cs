


using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class LoginModel
    {
        // optional for future use for updating users
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }

        public LoginModel()
        {
        }

        public LoginModel(string username, string password, string code)
        {

            Username = username;
            Password = password;
            Code = code;

        }

    }
}