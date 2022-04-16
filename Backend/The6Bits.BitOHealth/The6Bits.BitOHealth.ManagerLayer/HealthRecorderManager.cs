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

        public HealthRecorderResponseModel CreateRecord(HealthRecorderResponseModel response, string username, string categoryName, string recordName, IFormFile file, IFormFile? file2 = null)
        {
            if (!_HealthRecorderService.ValidateCategoryName(categoryName) || !_HealthRecorderService.ValidateRecordName(recordName))
            {
                response.ErrorMessage =  "invalid category/record name";
                return response;
            }
            
            if (!_HealthRecorderService.ValidateFileLength(file))
            {
               response.ErrorMessage = "file too big";
                return response;

            }
            

            if (!_HealthRecorderService.ValidateFileType(file))
            {
                response.ErrorMessage = "invalid file type";
                return response;
            }
            if (file2 != null)
            {
                if (!_HealthRecorderService.ValidateFileLength(file2))
                {
                    response.ErrorMessage = "file 2 too big";
                    return response;
                }
                if (!_HealthRecorderService.ValidateFileType(file2))
                {
                    response.ErrorMessage = "invalid file 2 type";
                    return response;
                }
            }
            string userLimitCheck = _HealthRecorderService.ValidateUserRecordRequest(username, recordName);
            if (userLimitCheck.Contains("Database") || userLimitCheck != "valid request")
            {
                response.ErrorMessage = userLimitCheck;
                return response;
            }

            string savedRecord = _HealthRecorderService.SaveRecord(file,username, categoryName, recordName, file2);

            if (savedRecord.Contains("Database"))
            {
                response.ErrorMessage = savedRecord;
                return response;
            }
            response.Data = "Record Saved";
            return response;
        }
    public HealthRecorderViewRecordModel ViewRecord(string username, int lastRecordIndex)
        {
            List<HealthRecorderRecordModel> rawData = _HealthRecorderService.ViewRecord(username, lastRecordIndex);
            HealthRecorderViewRecordModel wrapperData = new HealthRecorderViewRecordModel();
            if (rawData.Count == 0)
            {
                return wrapperData;
            }
            else
            {
                if (rawData[0].ErrorCode != null)
                {
                    wrapperData.ErrorMessage = rawData[0].ErrorCode;
                    return wrapperData;
                }
                else
                {
                    wrapperData.Records = rawData;
                    return wrapperData;
                }
            }
            
        }
        public HealthRecorderResponseModel DeleteRecord(HealthRecorderRequestModel request, HealthRecorderResponseModel response, string username)
        {
            response = _HealthRecorderService.ValidateRecordExists(request, response, username);
            if (response.Data == "0" || response.Data == null)
            {
                if (response.ErrorMessage == null)
                {
                    response.ErrorMessage = "Record does not exist";
                    return response;
                }
                else
                {
                    return response;
                }
            }
            response = _HealthRecorderService.DeleteRecord(request, response, username);
            if (response.Data == "0" || response.Data == null)
            {
                if (response.ErrorMessage == null)
                {
                    response.Data = "No Records Deleted";
                    return response;
                }
                else
                {
                    return response;
                }
            }
            response.Data = "Record Deleted Successfully";

            return response;
        }
        public HealthRecorderViewRecordModel SearchRecord (HealthRecorderRequestModel request, HealthRecorderViewRecordModel response, string username)
        {
            //add error cases
            response = _HealthRecorderService.SearchRecord(request, response, username);
            return response;
        }
    }
}
