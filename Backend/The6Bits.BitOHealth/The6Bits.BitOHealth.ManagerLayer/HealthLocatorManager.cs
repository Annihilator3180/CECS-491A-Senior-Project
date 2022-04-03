using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Authentication.Contract;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.ManagerLayer;

public class HealthLocatorManager
{
    private IAuthenticationService _authenticationService;
    private HealthLocatorService _HLS;

    public HealthLocatorManager(HealthLocatorService healthLocation)
    {
        _HLS = healthLocation;
    }
    
    public async Task<string> ViewHL()
    {
        return await _HLS.viewHL();
    }

    public async Task<string> SearchHL()
    {
        return await _HLS.searchHL();
    }
}
