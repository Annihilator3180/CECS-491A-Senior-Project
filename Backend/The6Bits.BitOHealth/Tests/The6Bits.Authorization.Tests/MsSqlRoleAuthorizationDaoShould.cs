using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using The6Bits.Authorization.Contract;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using Xunit;

namespace The6Bits.Authorization.Tests
{
    public class MsSqlRoleAuthorizationDaoShould : TestsBase
    {
        private IAuthorizationDao _authorizationDao;
        public MsSqlRoleAuthorizationDaoShould()
        {
            _authorizationDao = new MsSqlRoleAuthorizationDao(conn);
        }



        [Theory]
        [InlineData("bossadmin12")]
        public void getClaims(string username)
        {


            var claims = _authorizationDao.getClaims(username);



            Assert.True( claims.HasClaim("IsAdmin", "1"));
        }


    }
}
