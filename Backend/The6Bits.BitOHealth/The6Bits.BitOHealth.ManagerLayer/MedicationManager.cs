using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Authentication.Contract;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using The6Bits.Logging.DAL.Contracts;
using The6Bits.Logging.Implementations;

namespace The6Bits.BitOHealth.ManagerLayer;

public class MedicationManager
{
    private IAuthenticationService _auth;
    private MedicationService _MS;
    private IDBErrors _iDBErrors;
    private LogService _log;
    private ReminderManager _reminderManager;



    public MedicationManager(IRepositoryMedication<string> MedicationDao, IDrugDataSet _drugDataSet, IAuthenticationService authenticationService, 
        IDBErrors dbError, IConfiguration config, ILogDal logDao, ReminderManager rm)
    {
        _iDBErrors = dbError;
        _auth = authenticationService;
        _log= new LogService(logDao); ;
        _MS = new MedicationService(MedicationDao, _drugDataSet, dbError);
        _reminderManager = rm;
    }

    public FindDrugResponse FindDrug(string username,string drugName)
    {
        FindDrugResponse drugResponse=new FindDrugResponse();
        List<DrugName> genericDrugNames = _MS.GetGenericDrugName(drugName);
        List<DrugName> brandDrugNames = _MS.GetBrandDrugName(drugName);
        List<DrugName> drugNames = _MS.CheckDuplicates(genericDrugNames, brandDrugNames);
        if (drugNames.Count == 0)
        {
            drugResponse.success = false;
            drugResponse.error = "no drugs found";
            return drugResponse;
        }
        _ =_log.Log(username, "Searched for", "Front End", "Business");
        drugResponse.success = true;
        drugResponse.data = drugNames;
        return drugResponse;
    }

    public string addFavorite(DrugName drug, string username)
    {
        try
        {
            _MS.addFavorite(username, drug);
            _ = _log.Log(username, "Add Favorite drug" + drug.brand_name, "Data Store", "Error");
            return "Favorited";
        }
        catch (Exception ex)
        {
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
            _ =_log.Log(username, "FavoriteDrugs Database Error " + dbError, "Data Store", "Error");
            return "Database Error";
        }
        
    }

    public ViewFavoriteRequest ViewFavorite(string username)
    {
        ViewFavoriteRequest requestResult=new ViewFavoriteRequest();
        try
        {
            requestResult.data= _MS.ViewFavorites(username);
            requestResult.isSuccess = true;
            return requestResult;

         }
        catch(Exception ex)
        {
            requestResult.isSuccess = false;
            if (ex.Message.Any(Char.IsLetter))
            {
                _ = _log.Log(username, ex.Message, "Back End", "Business");
                requestResult.Error = ex.Message;
            }
            else
            {
                string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
                _log.Log(username, "FavoriteDrugs Database Error " + dbError, "Data Store", "Error");
                requestResult.Error = "Database Error";
            }
            return requestResult;
        }
        
    }

    public string RemoveFavorite(string drugProductID, string username)
    {
        try
        {
            string favoriteRemoved= _MS.RemoveFavorite(drugProductID, username);
            _ = _log.Log(username, favoriteRemoved +drugProductID, "Data Store", "Business");
            return favoriteRemoved;
        }
        catch (Exception ex)
        {
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
            _log.Log(username, "Delete Favorite Drugs Database Error " + dbError, "Data Store", "Error");
            return "Database Error";
        }
    }

    public string UpdateFavorite(string username, FavoriteDrug favoriteMedication)
    {
        if (!_MS.ValidateLocation(favoriteMedication.lowestPriceLocation))
        {
            _log.Log(username, "Invalid Location", "Front End", "Business");
            return "Invalid Location";
        }
        if (!_MS.ValidatePrice(favoriteMedication.lowestprice))
        {
            _log.Log(username, "Invalid Price", "Front End", "Business");
            return "Invalid Price";
        }
        try
        {
            string updatedFavorite = _MS.updateFavorite(username, favoriteMedication);
            _log.Log(username, updatedFavorite +" "+ favoriteMedication.generic_name, "Front End", "Business");
            return updatedFavorite;
        }
        catch( Exception ex)
        {
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
            _log.Log(username, "Favorite Medicine Database Error" + dbError, "Data Store", "Error");
            return "Database Error";
        }




    }

    public drugInfoResponse ViewDrug(string username, string generic_name)
    {
        drugInfoResponse infoResponse = new drugInfoResponse();
        try
        {
            drugInfo drug = _MS.ViewDrug(generic_name);
            try
            {

                drugInfo drugFavoriteInformation = _MS.makeFavorite(username, drug);
                infoResponse.isSuccess = true;
                infoResponse.data = drugFavoriteInformation;
                return infoResponse;
            }
            catch (Exception ex)
            {
                string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
                _log.Log(username, "FavoriteDrugs Database Error " + dbError, "Data Store", "Error");
            }
            infoResponse.isSuccess = true;
            infoResponse.data = drug;
            return infoResponse;

        }
        catch( Exception ex)
        {
                _ = _log.Log(username, ex.Message, "Back End", "Business");
                infoResponse.isSuccess = false;
                infoResponse.Error = ex.Message;
                return infoResponse;
        }
    }

    public string RefillMedication(string username, string name, string description, string date, string time, string repeat)
    {
        name = _MS.CreateTitle(name);
        description = _MS.CreateDescrption(description);
        string reminderSuccess=_reminderManager.CreateReminder(username, name, description, date, time, repeat);
        return reminderSuccess;
    }
}

