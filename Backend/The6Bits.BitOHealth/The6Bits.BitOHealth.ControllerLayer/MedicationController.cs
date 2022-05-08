
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
    private IAuthenticationService _auth;
 
    public MedicationController(IRepositoryMedication<string> MedicationDao,IDrugDataSet _drugDataSet, ILogDal logDao,
        IAuthenticationService authenticationService, IDBErrors dbErrors, IReminderDatabase remindDB)
    {
        _MM = new MedicationManager(MedicationDao, _drugDataSet , dbErrors,  logDao, new ReminderManager(remindDB, dbErrors));
        _auth = authenticationService;
 
    }
    [HttpGet("Search")]
    public FindDrugResponse FindDrug(string drugName)
    {
        FindDrugResponse drugResponse=new FindDrugResponse();
        string? token;
        try
        {

            token = Request.Headers["Authorization"];
            if (token == null){
                throw new Exception();
            }
            token = token.Split(' ')[1];
            if (!_auth.ValidateToken(token))
            {
                throw new Exception();
            }
            string username = _auth.getUsername(token);
            drugResponse = _MM.FindDrug(username, drugName);
            return drugResponse;

        }
        catch
        {
            drugResponse.success = false;
            drugResponse.error = "invalid token";
            return drugResponse;
        }


    }
    [HttpPost("FavoriteAdd")]
    public string AddFavorites(string genericName, string brandName, string product_ndc)
    {
        string? token;
        try
        {
            token = Request.Headers["Authorization"];
            if (token == null)
            {
                throw new Exception();
            }
            token = token.Split(' ')[1];
            if (!_auth.ValidateToken(token))
            {
                return "invalid token";
            }
            string username = _auth.getUsername(token);
            DrugName drugName = new DrugName(genericName, product_ndc, brandName);
            string favoriteAddResult = _MM.addFavorite(drugName, username);
            return favoriteAddResult;
        }
        catch
        {
            return "invalid token";
        }

    }

    [HttpPost("FavoriteView")]
    public ViewFavoriteRequest ViewFavorites()
    {
        string? token;
        ViewFavoriteRequest requestResult = new ViewFavoriteRequest();
        try
        {
            token = Request.Headers["Authorization"];
            if (token == null)
            {
                throw new Exception();
            }
            token = token.Split(' ')[1];
            if (!_auth.ValidateToken(token))
            {
                throw new Exception();
            }
            string username = _auth.getUsername(token);
            requestResult = _MM.ViewFavorite(username);
            return requestResult;

        }
        catch
        {
            requestResult.Error = "invalid token";
            requestResult.isSuccess = false;
            return requestResult;
        }

        




    }
    [HttpPost("DeleteFavorite")]
    public string RemoveFavorite(string product_ndc)
    {
        string? token;
        try
        {
            token = Request.Headers["Authorization"];
            if(token == null)
            {
                throw new Exception();
            }
            token = token.Split(' ')[1];
            if (!_auth.ValidateToken(token))
            {
                return "invalid token";
            }

            string username = _auth.getUsername(token);
            string deleteFavorite = _MM.RemoveFavorite(product_ndc, username);
            return deleteFavorite;
        }
        catch
        {
            return "invalid token";
        }

        

    }
    [HttpPost("viewDrug")]
    public drugInfoResponse ViewDrug(string brand_name)
    {
        drugInfoResponse infoResponse=new drugInfoResponse();
        string? token;
        try
        {
            token = Request.Headers["Authorization"];
            if (token == null)
            {
                throw new Exception();
            }
            token = token.Split(' ')[1];
            if (!_auth.ValidateToken(token))
            {
                throw new Exception();
            }

            string username = _auth.getUsername(token);
            infoResponse = _MM.ViewDrug(username, brand_name);
            return infoResponse;

        }
        catch
        {
            infoResponse.Error = "invalid token";
            infoResponse.isSuccess = false;
            return infoResponse;
        }

        

    }

    [HttpPost("UpdateFavorite")]
    public string UpdateFavorite(FavoriteDrug favoriteMedication)
    {
        string? token;
        try
        {
            token = Request.Headers["Authorization"];
            if (token == null)
            {
                throw new Exception();
            }
            token = token.Split(' ')[1];
            if (!_auth.ValidateToken(token))
            {
                return "invalid token";
            }
            string username = _auth.getUsername(token);
            string updatedFavorite = _MM.UpdateFavorite(username, favoriteMedication);
            return updatedFavorite;
        }
        catch
        {
            return "invalid token";
        }



        

    }
    [HttpPost("Reminder")]
    public string RefillMedication(string name, string description, string date, string time, string repeat)
    {
        string? token;
        
        try
        {
            token = Request.Headers["Authorization"];
            if (token == null)
            {
                throw new Exception();
            }
            token = token.Split(' ')[1];
            if (!_auth.ValidateToken(token))
            {
                return "invalid token";
            }

            string username = _auth.getUsername(token);
            string RefillMedication = _MM.RefillMedication(username, name, description, date, time, repeat);
            return RefillMedication;
        }
        catch
        {
            return "invalid token";
        }

    }
}
