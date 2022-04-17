
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
using The6Bits.BitOHealth.DAL.Contract;

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
    private ReminderManager _ReminderManager;
    private IReminderDatabase _reminderDB;
    public MedicationController(IRepositoryMedication<string> MedicationDao,IDrugDataSet _drugDataSet, ILogDal logDao,
        IAuthenticationService authenticationService, IDBErrors dbErrors, IReminderDatabase remindDB,
         IConfiguration config)
    {
        _ReminderManager = new ReminderManager(remindDB, dbErrors);
        _MM = new MedicationManager(MedicationDao, _drugDataSet, authenticationService, dbErrors, config, logDao,_ReminderManager);//RM
        _logService = new LogService(logDao);
        _dbErrors = dbErrors;
        _auth = authenticationService;
        _config = config;
    }
    [HttpGet("Search")]
    public FindDrugResponse FindDrug(string drugName)
    {
        FindDrugResponse drugResponse=new FindDrugResponse();
        string token;
        try
        {

            token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];

        }
        catch
        {
            drugResponse.success = false;
            drugResponse.error = "invalid token";
            return drugResponse;
        }
        if (!_auth.ValidateToken(token))
        {
            drugResponse.success = false;
            drugResponse.error = "invalid token";
            return drugResponse;
        }
        string username = _auth.getUsername(token);
        drugResponse = _MM.FindDrug(username,drugName);
        return drugResponse;

    }
    [HttpPost("FavoriteAdd")]
    public string AddFavorites(string genericName, string brandName, string productID)
    {
        string token;
        try
        {
            token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
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
    public ViewFavoriteRequest ViewFavorites()
    {
        string token;
        ViewFavoriteRequest requestResult = new ViewFavoriteRequest();
        try
        {
            token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
            if (!_auth.ValidateToken(token))
            {
                requestResult.Error = "invalid token";
                requestResult.isSuccess = false; 
                return requestResult;
            }
        }
        catch
        {
            requestResult.Error = "invalid token";
            requestResult.isSuccess = false;
            return requestResult;
        }
        string username = _auth.getUsername(token);
        requestResult = _MM.ViewFavorite(username);
        return requestResult;
        




    }
    [HttpPost("DeleteFavorite")]
    public string RemoveFavorite(string product_id)
    {
        string token;
        try
        {
            token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
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
            string deleteFavorite = _MM.RemoveFavorite(product_id, username);
            return deleteFavorite;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    [HttpPost("viewDrug")]
    public drugInfoResponse ViewDrug(string generic_name)
    {
        drugInfoResponse infoResponse=new drugInfoResponse();
        string token;
        try
        {
            token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
        }
        catch
        {
            infoResponse.Error = "invalid token";
            infoResponse.isSuccess = false;
            return infoResponse;
        }
        if (!_auth.ValidateToken(token))
        {
            infoResponse.Error = "invalid token";
            infoResponse.isSuccess = false;
            return infoResponse;
        }

        string username = _auth.getUsername(token);
        infoResponse = _MM.ViewDrug(username, generic_name);
        return infoResponse;
        
        

    }

    [HttpPost("UpdateFavorite")]
    public string UpdateFavorite(FavoriteDrug favoriteMedication)
    {
        string token;
        try
        {
            token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
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
    [HttpPost("Reminder")]
    public string RefillMedication(string name, string description, string date, string time, string repeat)
    {
        string token;
        
        try
        {
            token = Request.Headers["Authorization"];
            token = token.Split(' ')[1];
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
            string RefillMedication = _MM.RefillMedication(username, name, description, date, time, repeat);
            return RefillMedication;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
}
