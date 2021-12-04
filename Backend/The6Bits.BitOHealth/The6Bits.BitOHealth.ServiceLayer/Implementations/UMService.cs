using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer.Contract;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class UMService : IUMService
    {
        private IRepositoryUM<User> _dao;

        public UMService(IRepositoryUM<User> daoType)
        {
            _dao = daoType;
        }

        public bool ValidateEmail(string email)
        {
            try
            {
                
                return new EmailAddressAttribute().IsValid(email) && email.Length < 255;
            }
            catch
            {
                return false;
            }
        }

        //TODO: DOUBLE CHECK VALIDATIONS
        public bool ValidatePassword(string password)
        {
            try
            {
                return password.Any(char.IsUpper) & password.Any(char.IsLower) & password.Any(char.IsDigit) &
                       password.Length >= 8 & password.Length <= 30;
            }
            catch
            {
                return false;
            }
        }

        //TODO: DOUBLE CHECK VALIDATIONS
        public string ValidateUsername(string username)
        {
            User user = new User()
            {
                Username = username
            };
            if (!username.Any(char.IsAscii) & username.Length < 15)
            {
                return "Invalid Username";
            }
            if (_dao.Read(user).Username == username)
            {
                return "username exists";
            }

            return "new username";
        }


        public string CreateAccount(User user)
        {
            return _dao.Create(user) ? "account created" : "database error";
        }

        //SOLID IF VALIDATEUSERNAME HERE?
        public string DeleteAccount(string username)
        {
            User user = new User()
            {
                Username = username
            };
            return _dao.Delete(user) ? "account Deleted" : "database error";
        }

        public string UpdateAccount(User user)
        {
            return _dao.Update(user) ? "account update" : "database error";
        }


        //SOLID IF VALIDATEUSERNAME HERE?
        public string EnableAccount(string username)
        {
            return _dao.EnableAccount(username) ? "account enabled" : "database error";
        }

        public string DisableAccount(string username)
        {
            return _dao.DisableAccount(username) ? "account disabled" : "database error";
        }

        public IList<string> BulkCreateRandom(int amount)
        {
            IList<string> usernames = new List<string>();
            User user = new User();
            if (amount < 9999)
            {
                user.Username = RandomString(9);
                user.IsAdmin = 0;
                foreach (int i in Enumerable.Range(0, amount))
                {
                    _dao.Create(user);
                    usernames.Add(user.Username);
                    user.Username = RandomString(9);
                }

                return usernames;
            }
            {
                return new List<string>();
            }
        }


        public bool BulkDelete(IList<string> usernames)
        {
            User u = new User();
            foreach (string username in usernames)
            {
                u.Username = username;
                _dao.Delete(u);
            }

            return true;
        }


        private string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            foreach (var i in Enumerable.Range(0,size))
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }





    }
}