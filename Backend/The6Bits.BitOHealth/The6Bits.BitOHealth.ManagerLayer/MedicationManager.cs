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

public class MedicationManager
{
    private IAuthenticationService _auth;
    private MedicationService _MS;
    private IDBErrors _iDBErrors;
    private IConfiguration _config;



    public MedicationManager(IRepositoryMedication<string> MedicationDao,IDrugDataSet _drugDataSet, IAuthenticationService authenticationService, IDBErrors dbError, IConfiguration config)
    {
        _iDBErrors = dbError;
        _auth = authenticationService;
        _config = config;
        _MS = new MedicationService(MedicationDao,_drugDataSet, dbError, config);
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
            bool isValidCount=_MS.getFavoriteCount(username);
            if (!isValidCount)
            {
                return "Favorite Limit Reached";
            }
        }
        catch(Exception ex)
        {
            return "Database Error";
        }
        try
        {
           _MS.addFavorite(username,drug);
        }
        catch (Exception ex)
        {
            return "Database Error";
        }
        return "Favorited";
    }
}
