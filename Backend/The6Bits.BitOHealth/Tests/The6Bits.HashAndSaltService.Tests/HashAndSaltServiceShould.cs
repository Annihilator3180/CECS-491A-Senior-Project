using System.Security.Cryptography.X509Certificates;

using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.HashAndSaltService;
using The6Bits.HashAndSaltService.Implementations;

namespace The6Bits.HashAndSaltService.Tests;

public class HashAndSaltServiceShould : TestsBase
{

    private HashNSaltService _service;
    public HashAndSaltServiceShould()
    {
        _service = new HashNSaltService(new MsSqlHashDao(conn), "");
    }

    
    
    
    
}