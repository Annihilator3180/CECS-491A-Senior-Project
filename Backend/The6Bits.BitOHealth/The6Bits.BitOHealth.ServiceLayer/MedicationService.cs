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
        private IDrugDataSet _drugDataSet;

        private IRepositoryMedication<string> _MedicationDao;

        public MedicationService(IRepositoryMedication<string> MedicationDao, IDrugDataSet drugDataSet)
        {
            _drugDataSet=drugDataSet;
            _MedicationDao=MedicationDao;
    }



        public List<DrugName> CheckDuplicates(List<DrugName> genericDrugNames, List<DrugName> brandDrugNames)
        {
            List<string> uniqueDrugIDs=new List<string>();
            List<string> uniqueGenericName=new List<string>(); 
            List<string> uniqueBrandName=new List<string>();
            if (genericDrugNames.Count == 1 && genericDrugNames[0].product_ndc == "")
            {
                genericDrugNames.Clear();
            }
            if (brandDrugNames.Count == 1 && brandDrugNames[0].product_ndc == "")
            {
                brandDrugNames.Clear();
            }
            List<DrugName> drugNames = genericDrugNames.Concat(brandDrugNames).ToList();
            int i=0;
            while(i<drugNames.Count)
            {
                if (uniqueDrugIDs.Contains(drugNames[i].product_ndc) || uniqueGenericName.Contains(drugNames[i].generic_name)
                    || uniqueBrandName.Contains(drugNames[i].brand_name))
                {
                    drugNames.RemoveAt(i);
                }
                else
                {
                    uniqueDrugIDs.Add(drugNames[i].product_ndc);
                    uniqueBrandName.Add(drugNames[i].brand_name);
                    uniqueGenericName.Add(drugNames[i].generic_name);
                    i++;
                }
            }
            return drugNames;

        }

        public bool isFavorited(string username, string drugName)
        {
            FavoriteDrug favorited = _MedicationDao.Read(username, drugName);
            return favorited.generic_name != "";
            
            
        }

        public bool DeleteFavoriteList(string username)
        {
            return _MedicationDao.DeleteUsersList(username) > 0;
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

        public List<DrugName> GetGenericDrugName(string drugName)
        {
            return _drugDataSet.GetGenericDrugName(drugName).Result;
        }
        public List<DrugName> GetBrandDrugName(string drugName)
        {
            return _drugDataSet.GetBrandDrugName(drugName).Result;
        }

        public string RemoveFavorite(string product_ndc, string username)
        {
            int favoriteRemovalResult = _MedicationDao.RemoveFavorite(product_ndc, username);
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

        public bool ValidatePrice(double lowestprice)
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
            return favoriteCount < 100;
                
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

        public drugInfo ViewDrug(string generic_name)
        {
            return _drugDataSet.GetDrugInfo(generic_name).Result;
        }
        public FavoriteDrug Read(string username, string genericdrugName)
        {
            return _MedicationDao.Read(username, genericdrugName); 
        }

        public drugInfo makeFavorite(string username, drugInfo drug)
        {
            FavoriteDrug favorited = _MedicationDao.Read(username,drug.openfda!.generic_name![0]);
            if(favorited.generic_name != "")
            {
                drug.isFavorited = true;   
                drug.favoriteDrug = favorited;
            }
            else
            {
                drug.isFavorited = false;
            }
            return drug;
        }
        public bool validateDescription(string description)
        { 
            return description.Length<501;
        }

            public string CreateDescription(string description)
        {
            var splitted=description.Split('.');
            string location=splitted[0];
            string price=splitted[1];
            return "Cheapest reported price is " + price + " Found at " + location;
        }

        public string CreateTitle(string name)
        {
            return "Reminder: " + name + " refill "; 
        }
    }
}