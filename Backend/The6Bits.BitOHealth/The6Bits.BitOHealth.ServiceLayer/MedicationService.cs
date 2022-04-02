using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;
using The6Bits.EmailService;
using Microsoft.Extensions.Configuration;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class MedicationService
    {
        IDrugDataSet _drugDataSet;
        private IDBErrors _DBErrors;
        private IConfiguration _config;
        private IRepositoryMedication<string> _MedicationDao;
        public MedicationService(IRepositoryMedication<string> MedicationDao, IDrugDataSet drugDataSet, IDBErrors DbError,
             IConfiguration config)
        {
            _DBErrors = DbError;
            _drugDataSet = drugDataSet;
            _config = config;
            _MedicationDao = MedicationDao;



        }
        public MedicationService(IRepositoryMedication<string> MedicationDao, IDrugDataSet drugDataSet)
        {
        }


        public List<DrugName> GetGenericDrugName(string drugName)
        {
            return _drugDataSet.GetGenericDrugName(drugName).Result;
        }
        public List<DrugName> GetBrandDrugName(string drugName)
        {
            return _drugDataSet.GetBrandDrugName(drugName).Result;
        }

        public List<DrugName> CheckDuplicates(List<DrugName> genericDrugNames, List<DrugName> brandDrugNames)
        {
            List<string> uniqueDrugIDs=new List<string>();
            List<string> uniqueGenericName=new List<string>(); 
            List<string> uniqueBrandName=new List<string>();
            if (genericDrugNames.Count == 1 && genericDrugNames[0].product_id=="")
            {
                genericDrugNames.Clear();
            }
            if (brandDrugNames.Count == 1 && brandDrugNames[0].product_id == "")
            {
                brandDrugNames.Clear();
            }
            List<DrugName> drugNames = genericDrugNames.Concat(brandDrugNames).ToList();
            int i=0;
            while(i<drugNames.Count)
            {
                if (uniqueDrugIDs.Contains(drugNames[i].product_id)|| uniqueGenericName.Contains(drugNames[i].generic_name)
                    || uniqueBrandName.Contains(drugNames[i].brand_name))
                {
                    drugNames.RemoveAt(i);
                }
                else
                {
                    uniqueDrugIDs.Add(drugNames[i].product_id);
                    uniqueBrandName.Add(drugNames[i].brand_name);
                    uniqueGenericName.Add(drugNames[i].generic_name);
                    i++;
                }
            }
            return drugNames;

        }

        public List<FavoriteDrug> ViewFavorites(string username)
        {
            List<FavoriteDrug> favDrug=  _MedicationDao.ViewFavorites(username);
            if (favDrug.Count == 0)
            {
                throw new Exception("no drugs found");
            }
            return favDrug;
        }

        public string RemoveFavorite(string drugProductID, string username)
        {
            int favoriteRemovalResult = _MedicationDao.RemoveFavorite(drugProductID, username);
            if (favoriteRemovalResult == 1)
            {
                return "Deleted Favorite";
            }
            return "no matches found";
        }

        public bool addFavorite(string username, DrugName drug)
        {
            return _MedicationDao.addFavorite(username, drug);
        }

        public bool ValidatePrice(int lowestprice)
        {
            return lowestprice <9000000 && lowestprice >-1;
        }

        public bool ValidateLocation(string lowestPriceLocation)
        {
            return lowestPriceLocation.Length < 151;
        }

        public bool getFavoriteCount(string username)
        {
            int favoriteCount = _MedicationDao.getFavoriteCount(username);
            return favoriteCount < 10;
                
        }

        public string updateFavorite(string username, FavoriteDrug drug)
        {
            int updatedFavorite = _MedicationDao.UpdateFavorite(username, drug);
            if (updatedFavorite == 1)
            {
                return "updated favorite";
            }
            return "No matches found";
        }

        public DrugInfo ViewDrug(string generic_name)
        {
            return _drugDataSet.GetDrugInfo(generic_name).Result;
        }
    }
}