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
    private IConfiguration _config;
    private LogService _log;



    public MedicationManager(IRepositoryMedication<string> MedicationDao, IDrugDataSet _drugDataSet, IAuthenticationService authenticationService, 
        IDBErrors dbError, IConfiguration config, ILogDal logDao)
    {
        _iDBErrors = dbError;
        _auth = authenticationService;
        _config = config;
        _log= new LogService(logDao); ;
        _MS = new MedicationService(MedicationDao, _drugDataSet, dbError, config);
    }

    public List<DrugName> FindDrug(string drugName)
    {
        List<DrugName> genericDrugNames = _MS.GetGenericDrugName(drugName);
        List<DrugName> brandDrugNames = _MS.GetBrandDrugName(drugName);
        List<DrugName> drugNames = _MS.CheckDuplicates(genericDrugNames, brandDrugNames);
        return drugNames;
    }

    public string addFavorite(DrugName drug, string username)
    {
        try
        {
            bool isValidCount = _MS.getFavoriteCount(username);
            if (!isValidCount)
            {
                return "Favorite Limit Reached";
            }
        }
        catch (Exception ex)
        {
            return "Database Error";
        }
        try
        {
            _MS.addFavorite(username, drug);
        }
        catch (Exception ex)
        {
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
            _log.Log(username, "FavoriteDrugs Database Error " + dbError, "Data Store", "Error");
            return "Database Error";
        }
        return "Favorited";
    }

    public List<FavoriteDrug> ViewFavorite(string username)
    {
        try
        {
            return _MS.ViewFavorites(username);
         }
        catch(Exception ex)
        {
            if (ex.Message == "no drugs found")
            {
                _ = _log.Log(username, "no Favorited Drugs", "Manager", "Business");
                throw new Exception(ex.Message);
            }
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message)); 
            _log.Log(username, "FavoriteDrugs Database Error "+ dbError, "Data Store", "Error");
            throw new Exception("Database Error");
            
        }
        
    }

    public string RemoveFavorite(string drugProductID, string username)
    {
        try
        {
            string favoriteRemoved= _MS.RemoveFavorite(drugProductID, username);
            _ = _log.Log(username, favoriteRemoved +drugProductID, "Manager", "Business");
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
        bool isValidLocation = _MS.ValidateLocation(favoriteMedication.lowestPriceLocation);
        if (!isValidLocation)
        {
            _log.Log(username, "Invalid Location", "Front End", "Business");
            return "Invalid Location";
        }
        bool isValidPrice = _MS.ValidatePrice(favoriteMedication.lowestprice);
        if (!isValidPrice)
        {
            _log.Log(username, "Invalid Price", "Front End", "Business");
            return "Invalid Price";
        }
        try
        {
            string updatedFavorite = _MS.updateFavorite(username, drug: favoriteMedication);
            _log.Log(username, updatedFavorite, "Front End", "Business");
            return updatedFavorite;
        }
        catch( Exception ex)
        {
            string dbError = _iDBErrors.DBErrorCheck(int.Parse(ex.Message));
            _log.Log(username, "Favorite Medicine Database Error" + dbError, "Data Store", "Error");
            return "Database Error";
        }




    }

    public drugInfo ViewDrug(string username, string generic_name)
    {
        drugInfo drug= _MS.ViewDrug(generic_name);
        drugInfo Favorited = _MS.makeFavorite(username, drug);
        return drug;
    }
}

