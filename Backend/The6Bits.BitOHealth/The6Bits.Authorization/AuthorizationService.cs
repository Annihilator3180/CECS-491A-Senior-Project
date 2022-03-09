using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic;
using The6Bits.Authorization.Contract;
using The6Bits.BitOHealth.Models;

namespace The6Bits.Authorization;

public class AuthorizationService
{
    private IAuthorizationDao _authorizationDao;

    public AuthorizationService(IAuthorizationDao iAuthorizationDao)
    {
        _authorizationDao = iAuthorizationDao;
    }

    public ClaimsIdentity getClaims(string username)
    {
        try
        {
            return _authorizationDao.getClaims(username);
        }
        catch (Exception ex)
        {
            return new ClaimsIdentity();
        }
    }

    public bool VerifyClaim(string token, string claim)
    {

        var parts = token.Split('.');
        var header = parts[0];
        var payload = parts[1];

        var data = Convert.FromBase64String((string)payload);

        var dataString = Encoding.UTF8.GetString(data);

        var user = JsonSerializer.Deserialize<JwtPayloadModel>(dataString);

        foreach (var c in user.Claims.Claims)
        {
            if (claim == c.Type && c.Value == "1")
            {
                return true;
            }
        }
        return false;
    }

}