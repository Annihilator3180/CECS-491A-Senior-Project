using The6Bits.Authorization.Contract;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ManagerLayer;

public class APIManager
{
    public IAuthorizationService auth;
    private JWT j;
    public string token;
    private APIService _AS;
    public APIManager(IRepositoryUM<User> daoType)
    {
        j = new JWT();
        _AS = new APIService(daoType);
    }

    public string Login(User user)
    {
        return j.generateToken(user.Username);
    }
}