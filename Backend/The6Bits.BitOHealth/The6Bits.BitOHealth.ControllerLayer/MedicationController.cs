using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.Authentication.Implementations;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;
using The6Bits.DBErrors;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using The6Bits.EmailService;
using System.Text.Json;
// using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Medication")]
public class MedicationController
{
    
    private MedicationManager _MM;
    private LogService logService;
    private IDBErrors _dbErrors;
    private IConfiguration _config;
    private IAuthenticationService _auth;
    public MedicationController(IDrugDataSet _drugDataSet, ILogDal logDao, IAuthenticationService authenticationService, IDBErrors dbErrors,
         IConfiguration config)
    {
        _MM = new MedicationManager(_drugDataSet, authenticationService, dbErrors, config);
        logService = new LogService(logDao);
        _dbErrors = dbErrors;
        _auth = authenticationService;
        _config = config;
    }
    [HttpGet("Search")]
    public string FindDrug(string drugName)
    {
        /**if (!_auth.ValidateToken(token))
        {
            return "invalid token";
        }
        string username = _auth.getUsername(token);**/
        List<DrugName> genericdrugNames = _MM.FindDrug(drugName);
        string jsonString = JsonSerializer.Serialize(genericdrugNames);
        return jsonString;

    }
}
