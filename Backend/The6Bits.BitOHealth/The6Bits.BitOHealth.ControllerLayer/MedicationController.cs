
using Microsoft.AspNetCore.Mvc;
using The6Bits.Authentication.Contract;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.ManagerLayer;
using The6Bits.BitOHealth.Models;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;
using The6Bits.DBErrors;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

// using The6Bits.BitOHealth.ServiceLayer;

namespace The6Bits.BitOHealth.ControllerLayer;
[ApiController]
[Route("Medication")]
public class MedicationController : ControllerBase
{
    
    private MedicationManager _MM;
    private LogService _logService;
    private IDBErrors _dbErrors;
    private IConfiguration _config;
    private IAuthenticationService _auth;
    public MedicationController(IRepositoryMedication<string> MedicationDao,IDrugDataSet _drugDataSet, ILogDal logDao,
        IAuthenticationService authenticationService, IDBErrors dbErrors,
         IConfiguration config)
    {
        _MM = new MedicationManager(MedicationDao,_drugDataSet, authenticationService, dbErrors, config, logDao);
        _logService = new LogService(logDao);
        _dbErrors = dbErrors;
        _auth = authenticationService;
        _config = config;
        _logService = new LogService(logDao); 
    }
    [HttpGet("Search")]
    public string FindDrug(string drugName)
    {
        string token;
        try
        {
            token = Request.Cookies["token"];

        }
        catch
        {
            return "invalid token";
        }
        if (!_auth.ValidateToken(token))
        {
            return "invalid token";
        }
        string username = _auth.getUsername(token);
        List<DrugName> genericdrugNames = _MM.FindDrug(drugName);
        string jsonString = JsonSerializer.Serialize(genericdrugNames);
        return jsonString;

    }
    [HttpPost("FavoriteAdd")]
    public string AddFavorites(string genericName, string brandName, string productID)
    {
        string token;
        try
        {
            token = Request.Cookies["token"];
        }
        catch
        {
            return "invalid token";
        }
        if (!_auth.ValidateToken(token))
        {
            return "invalid token";
        }
        string username = _auth.getUsername(token);
        DrugName drugName = new DrugName(genericName,productID,brandName);
        string favoriteAddResult= _MM.addFavorite(drugName, username);
        return favoriteAddResult;
    }

    [HttpPost("FavoriteView")]
    public string ViewFavorites()
    {
        string token;
        try
        {
            token = Request.Cookies["token"];
        }
        catch
        {
            return "invalid token";
        }
        if (!_auth.ValidateToken(token))
        {
            return "invalid token";
        }

        string username = _auth.getUsername(token);
        try
        {
            List<FavoriteDrug> favoriteDrugsList = _MM.ViewFavorite(username);
            string favoriteDrugs = JsonSerializer.Serialize(favoriteDrugsList);
            return favoriteDrugs;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        
    }
    [HttpPost("DeleteFavorite")]
    public string RemoveFavorite(FavoriteDrug favoriteMedication)
    {
        string token;
        try
        {
            token = Request.Cookies["token"];
        }
        catch
        {
            return "invalid token";
        }
        if (!_auth.ValidateToken(token))
        {
            return "invalid token";
        }

        string username = _auth.getUsername(token);
        try
        {
            string updatedFavorite = _MM.RemoveFavorite(username, favoriteMedication.product_id);
            return updatedFavorite;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    [HttpPost("viewDrug")]
    public string ViewDrug(string generic_name)
    {
        /**string token;
        try
        {
            token = Request.Cookies["token"];
        }
        catch
        {
            return "invalid token";
        }
        if (!_auth.ValidateToken(token))
        {
            return "invalid token";
        }

        string username = _auth.getUsername(token);**/
        try
        {
            DrugInfo drugInfo = _MM.ViewDrug("hello", generic_name);
            return JsonSerializer.Serialize(drugInfo);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    [HttpPost("UpdateFavorite")]
    public string UpdateFavorite(FavoriteDrug favoriteMedication)
    {
        string token;
        try
        {
            token = Request.Cookies["token"];
        }
        catch
        {
            return "invalid token";
        }
        if (!_auth.ValidateToken(token))
        {
            return "invalid token";
        }

        string username = _auth.getUsername(token);
        try
        {
            string updatedFavorite = _MM.UpdateFavorite(username,favoriteMedication);
            return updatedFavorite;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
}
