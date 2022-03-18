using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class HealthRecorderManager
    {
        private IDBErrors _dbErrors;
        private IRepositoryHealthRecorderDAO _dao;
        private HealthRecorderService _HRS;

        public HealthRecorderManager(IDBErrors dBErrors, IRepositoryHealthRecorderDAO dao)
        {
            _dbErrors = dBErrors;
            _dao = dao;
            _HRS = new HealthRecorderService(dao, dBErrors);
        }

        public string CreateRecord(string username, DateTime now, string categoryName, string recordName, IFormFile file, IFormFile? file2 = null)
        {
            if (!_HRS.ValidateCategoryName(categoryName) || !_HRS.ValidateRecordName(recordName))
            {
                return "invalid category/record name";
            }
            string userLimitCheck = _HRS.ValidateUserRecordLimit(username);
            if (userLimitCheck.Contains("Database") || userLimitCheck.Contains("over"))
            {
                return userLimitCheck;
            }
            string userDailyLimitCheck = _HRS.ValidateUserDailyRecordLimit(username, now);
            if(userDailyLimitCheck.Contains("Database") || userDailyLimitCheck.Contains("over"))
            {
                return userDailyLimitCheck;
            }
            if (!_HRS.ValidateFileLength(file))
            {
                return "file too big";
            }
            

            if (!_HRS.ValidateFileType(file))
            {
                return "invalid file type";
            }
            if (file2 != null)
            {
                if (!_HRS.ValidateFileLength(file2))
                {
                    return "file 2 too big";
                }
                if (!_HRS.ValidateFileType(file2))
                {
                    return "invalid file 2 type";
                }
            }

            string savedRecord = _HRS.SaveRecord(file,now,username, categoryName, recordName, file2);

            if (savedRecord.Contains("Database"))
            {
                return savedRecord;
            }
            return "Record Saved";
        }
    }
}
