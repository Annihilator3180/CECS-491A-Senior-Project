using Microsoft.AspNetCore.Mvc;
using The6Bits.Authorization.Contract;
using The6Bits.Authorization.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("API")]
public class API : ControllerBase
{
    private APIManager _AM;
    private JWT j;
    private LogService logService;
    public API(IRepositoryUM<User> daoType ,ILogDal logDao)
    {
        _AM = new APIManager(daoType);
        j =  new JWT();
        logService = new LogService(logDao);
    }

    [HttpPost("Login")]
    [Consumes("application/json")]
    public string Login(User u)
    {

        //Log stuff
        return _AM.Login(u);
    }
}