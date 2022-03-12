using System.Security.Cryptography.X509Certificates;

using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.HashAndSaltService;

namespace The6Bits.HashAndSaltService.Tests;

public class HashAndSaltServiceShould : TestsBase
{

    private HashAndSaltService _service;
    public HashAndSaltServiceShould()
    {
        _service = new HashAndSaltService("",new MsSqlHashDao(conn));
    }
    
    
    
    
}