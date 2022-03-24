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
            if (genericDrugNames.Count == 1 && genericDrugNames[0].product_id=="")
            {
                genericDrugNames.Clear();
            }
            if (brandDrugNames.Count == 1 && brandDrugNames[0].product_id == "")
            {
                brandDrugNames.Clear();
            }
            List<DrugName> drugNames = genericDrugNames.Concat(brandDrugNames).ToList();
            for (int i = 0;i<drugNames.Count; i++)
            {
                if (uniqueDrugIDs.Contains(drugNames[i].product_id))
                {
                    drugNames.RemoveAt(i);
                }
                else
                {
                    uniqueDrugIDs.Add(drugNames[i].product_id);
                }
            }
            return drugNames;

        }

        public bool addFavorite(string username, DrugName drug)
        {
            return _MedicationDao.addFavorite(username, drug);
        }

        public bool getFavoriteCount(string username)
        {
            int favoriteCount = _MedicationDao.getFavoriteCount(username);
            if (favoriteCount > 9)
            {
                return false;
            }
            return true;
                
        }
    }
}