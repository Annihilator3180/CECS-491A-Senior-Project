using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ManagerLayer;

public class HotTopicsManager
{
    private IAuthenticationService _auth;
    private HotTopicsService _HTS;

    public HotTopicsManager(HotTopicsService hotTopics)
    {
        _HTS = hotTopics;
    }

    public async Task<string>ViewHT()
    {
        return await _HTS.viewHT();
    }

}