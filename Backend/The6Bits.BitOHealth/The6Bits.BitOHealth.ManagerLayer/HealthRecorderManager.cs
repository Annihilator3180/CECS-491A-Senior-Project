using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.DBErrors;


namespace The6Bits.BitOHealth.ManagerLayer
{
    public class HealthRecorderManager
    {
        private IDBErrors _dbErrors;
        private IRepositoryHealthRecorderDAO _dao;
        private HealthRecorderService _HealthRecorderService;

        public HealthRecorderManager(IDBErrors dBErrors, IRepositoryHealthRecorderDAO dao)
        {
            _dbErrors = dBErrors;
            _dao = dao;
            _HealthRecorderService = new HealthRecorderService(dao, dBErrors);
        }

        public string CreateRecord(string username, DateTime now, string categoryName, string recordName, IFormFile file, IFormFile? file2 = null)
        {
            if (!_HealthRecorderService.ValidateCategoryName(categoryName) || !_HealthRecorderService.ValidateRecordName(recordName))
            {
                return "invalid category/record name";
            }
            
            if (!_HealthRecorderService.ValidateFileLength(file))
            {
                return "file too big";
            }
            

            if (!_HealthRecorderService.ValidateFileType(file))
            {
                return "invalid file type";
            }
            if (file2 != null)
            {
                if (!_HealthRecorderService.ValidateFileLength(file2))
                {
                    return "file 2 too big";
                }
                if (!_HealthRecorderService.ValidateFileType(file2))
                {
                    return "invalid file 2 type";
                }
            }
            string userLimitCheck = _HealthRecorderService.ValidateUserRecordLimit(username);
            if (userLimitCheck.Contains("Database") || userLimitCheck != "under limit")
            {
                return userLimitCheck;
            }

            string savedRecord = _HealthRecorderService.SaveRecord(file,now,username, categoryName, recordName, file2);

            if (savedRecord.Contains("Database"))
            {
                return savedRecord;
            }
            return "Record Saved";
        }
    public HealthRecorderViewRecordModel ViewRecord(string username, int lastRecordIndex)
        {
            List<string> rawData = _HealthRecorderService.ViewRecord(username, lastRecordIndex);
            return new HealthRecorderViewRecordModel(rawData);
        }
    }
}
