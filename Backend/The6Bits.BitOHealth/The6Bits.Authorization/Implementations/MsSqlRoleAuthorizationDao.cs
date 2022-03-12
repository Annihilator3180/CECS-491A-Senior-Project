using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using The6Bits.Authorization.Contract;
using The6Bits.BitOHealth.Models;

namespace The6Bits.Authorization.Implementations
{
    public class MsSqlRoleAuthorizationDao : IAuthorizationDao
    {
        private string _connectString;

        public MsSqlRoleAuthorizationDao(string connectString)
        {
            _connectString = connectString;

        }

        public ClaimsIdentity getClaims(string username)
        {

            string query = $"select * from Accounts where username = '{username}'; ";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectString))
                {
                    connection.Open();
                    IEnumerable<User> us = connection.Query<User>($"select * from Accounts where username = '{username}'; ");
                    User u =  us.First();
                    ClaimsIdentity claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim("IsAdmin",u.IsAdmin.ToString()));
                    claims.AddClaim(new Claim("IsEnabled", u.IsEnabled.ToString()));
                    claims.AddClaim(new Claim("privOption", u.privOption.ToString()));
                    return claims;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ErrorCode.ToString());

            }
        }




    }
}
