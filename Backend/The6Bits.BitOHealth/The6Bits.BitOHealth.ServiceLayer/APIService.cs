using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.ServiceLayer;

public class APIService
{
    private IRepositoryUM<User> _dao;

    public APIService(IRepositoryUM<User> daoType)
    {
        _dao = daoType;
    }
}