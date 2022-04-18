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
    private MedicationService _MS;
    private IDBErrors _iDBErrors;
    private LogService _log;
    private ReminderManager _reminderManager;



    public MedicationManager(IRepositoryMedication<string> MedicationDao, IDrugDataSet _drugDataSet,  
        IDBErrors dbError, ILogDal logDao, ReminderManager rm)
    {
        _iDBErrors = dbError;
        _log= new LogService(logDao); ;
        _MS = new MedicationService(MedicationDao, _drugDataSet);
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
            _ = _log.Log(username, "Searched for" + drugName, "Front End", "Business");
            return drugResponse;
        }
        _ =_log.Log(username, "Searched for"+ drugName, "Front End", "Business");
        drugResponse.success = true;
        drugResponse.data = drugNames;
        return drugResponse;
    }

    public string addFavorite(DrugName drug, string username)
    {
        try
        {
            if(!_MS.isFavorited(username, drug.generic_name)){
                _MS.addFavorite(username, drug);
                _ = _log.Log(username, "Add Favorite drug" + drug.brand_name, "Data Store", "Business");
                return "Favorited";
            }
            _ = _log.Log(username, "Already Favorite drug" + drug.brand_name, "Data Store", "Business");
            return "already favorited";
        }
        catch (Exception ex)
        {
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
            _ =_log.Log(username, "Favorite Drug Database Error " + dbError, "Data Store", "Error");
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
            _ = _log.Log(username, "FavoriteDrugs viewed list " , "Data Store", "Business");
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
                _= _log.Log(username, "FavoriteDrugs Database Error " + dbError, "Data Store", "Error");
                requestResult.Error = "Database Error";
            }
            return requestResult;
        }
        
    }

    public string RemoveFavorite(string product_ndc, string username)
    {
        try
        {
            string favoriteRemoved= _MS.RemoveFavorite(product_ndc, username);
            _ = _log.Log(username, favoriteRemoved + product_ndc, "Data Store", "Business");
            return favoriteRemoved;
        }
        catch (Exception ex)
        {
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
            _ = _log.Log(username, "Delete Favorite Drugs Database Error " + dbError, "Data Store", "Error");
            return "Database Error";
        }
    }

    public string UpdateFavorite(string username, FavoriteDrug favoriteMedication)
    {
        if (!_MS.ValidateLocation(favoriteMedication.lowestPriceLocation))
        {
            _ = _log.Log(username, "Invalid Location", "Front End", "Business");
            return "Invalid Location";
        }
        if (!_MS.ValidatePrice(favoriteMedication.lowestprice))
        {
            _ = _log.Log(username, "Invalid Price", "Front End", "Business");
            return "Invalid Price";
        }
        if (!_MS.validateDescription(favoriteMedication.description))
        {
            _ = _log.Log(username, "Invalid Description", "Front End", "Business");
            return "Invalid Description";
        }
        try
        {
            string updatedFavorite = _MS.updateFavorite(username, favoriteMedication);
            _ =_log.Log(username, updatedFavorite +" "+ favoriteMedication.generic_name, "Front End", "Business");
            return updatedFavorite;
        }
        catch( Exception ex)
        {
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
            _ =_log.Log(username, "Favorite Medicine Database Error" + dbError, "Data Store", "Error");
            return "Database Error";
        }




    }

    public drugInfoResponse ViewDrug(string username, string brand_name)
    {
        drugInfoResponse infoResponse = new drugInfoResponse();
        try
        {

            drugInfo? drug = _MS.ViewDrug(brand_name);
            if (drug.openfda is null){
                drug = _MS.ViewDrugGeneric(brand_name);
                 if (drug.openfda is null)
                {
                    throw new Exception("error getting drug");
                }
                    }
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
                _ =_log.Log(username, "FavoriteDrugs Database Error " + dbError, "Data Store", "Error");
            }
            _ = _log.Log(username, "FavoriteDrugs viewed" + brand_name, "Front End", "Business");
            infoResponse.isSuccess = true;
            infoResponse.data = drug;
            return infoResponse;

        }
        catch( Exception ex)
        {
                _ = _log.Log(username, "FavoriteDrugs couldn't view" + brand_name, "Front End", "Error");
                infoResponse.isSuccess = false;
                infoResponse.Error = ex.Message;
                return infoResponse;
        }
    }

    public string RefillMedication(string username, string name, string description, string date, string time, string repeat)
    {
        
        name = _MS.CreateTitle(name);
        description = _MS.CreateDescription(description);
        string reminderSuccess=_reminderManager.CreateReminder(username, name, description, date, time, repeat);
        _ = _log.Log(username, "Medication Reminder" + name, "Front End", "Business");
        return reminderSuccess;
    }
}

