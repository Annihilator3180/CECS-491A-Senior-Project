using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;


namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class AuthMsSqlDao : IRepositoryAuth<string>
    {
        private string _connectString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        public AuthMsSqlDao()
        {
        }

        public AuthMsSqlDao(string connectstring)
        {
            _connectString = connectstring;
        }

        public string UsernameExists(string username)
        {
            throw new NotImplementedException();
        }

        public string UserRole(string username)
        {
            throw new NotImplementedException();
        }

        public string CheckPassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
