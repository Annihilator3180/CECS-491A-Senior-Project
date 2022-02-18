


using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class LoginModel
    {
        // optional for future use for updating users
        
        public string Username { get; set; }
        public string Password { get; set; }
        
        public LoginModel()
        {
        }

        public LoginModel(string username, string password)
        {

            Username = username;
            Password = password;
            
        }

    }
}