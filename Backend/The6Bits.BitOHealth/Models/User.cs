


using System.Security;

namespace The6Bits.BitOHealth.Models
{
    public class User
    {
        // optional for future use for updating users
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }  
        public string Password { get; set; }
        public int IsEnabled { get; set; }

        public int IsAdmin { get; set; }

        public User()
        {
        }

        public User(string username, string email, string password, string first, string last, int isEnabled, int isAdmin)
        {
            FirstName = first;
            LastName = last;
            Username = username;
            Email = email;
            Password = password;
            isAdmin = IsAdmin;
            isEnabled = isEnabled;
        }

    }
}