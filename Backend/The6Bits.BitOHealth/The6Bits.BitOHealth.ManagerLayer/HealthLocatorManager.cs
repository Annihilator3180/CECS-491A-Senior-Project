using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapAPI;
using MapAPI.Contracts;
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
    private readonly IMapAPI<Parsed> _mapAPI;
    public HealthLocatorManager(IMapAPI<Parsed> mapAPIService)
    {

        _mapAPI = mapAPIService;

    }
    public HealthLocatorManager(HealthLocatorService healthLocationService, IMapAPI<Parsed> mapAPIService)
    {
        _HLS = healthLocationService;
        _mapAPI = mapAPIService;

    }
    
    public async Task<string> ViewHL()
    {
        return await _HLS.viewHL();
    }

    public async Task<string> SearchHL()
    {
        return await _HLS.searchHL();
    }

    public async Task<IEnumerable<Parsed>> SearchHL(string queryString)
    {
        return await _mapAPI.QueryLocations(queryString);
    }
}
