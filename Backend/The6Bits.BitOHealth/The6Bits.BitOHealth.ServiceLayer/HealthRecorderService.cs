using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.BitOHealth.Models;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class HealthRecorderService
    {
        private IRepositoryHealthRecorderDAO _HealthRecorderDAO;
        private IDBErrors _dbError;

        public HealthRecorderService(IRepositoryHealthRecorderDAO dao, IDBErrors dBErrors)
        {
            _HealthRecorderDAO = dao;
            _dbError = dBErrors;
        }
        public bool ValidateRecordName(string recordName)
        {

            return (recordName.Length >= 1 && recordName.Length <= 100);
        }
        public bool ValidateCategoryName(string categoryName)
        {
            return (categoryName.Length >= 1 && categoryName.Length <= 100);
        }
        public string ValidateUserRecordRequest(string username, string recordName) 
        {
            string result = _HealthRecorderDAO.ValidateUserRecordRequest(username, recordName);
            
            if (result == "over record limit" || result == "over daily limit" || result == "valid request" || result == "duplicate record name")
            {
                return result;
            }
            return _dbError.DBErrorCheck(int.Parse(result));
        }
   
        public bool ValidateFileLength(IFormFile file)
        {
            double maxSize = 16;
            double size = ByteToMegabyte(file.Length);

            return size <= maxSize;

        }
        public double ByteToMegabyte(double byteSize)
        {
            //appending f suffix creates a float
            return (byteSize / 1024f) / 1024f;
        }
        public bool ValidateFileType(IFormFile file)
        {
            string type = file.ContentType;
            string[] typeSplit = type.Split('/');
            return (typeSplit[1] == "jpeg" || typeSplit[1] == "pdf");
        }
        public string SaveRecord(IFormFile file, string username, string categoryName, string recordName, IFormFile? file2 = null)
        {
            //add string to separate 2 records and parse later
            string conversion = ConvertFileToString(file);
            string secondConversion = string.Empty;
            if (file2 != null)
            {
                secondConversion = ConvertFileToString(file2);
            }
            string result = _HealthRecorderDAO.SaveRecord(conversion, username, categoryName, recordName, secondConversion);
            if(result == "saved")
            {
                return result;
            }
            return _dbError.DBErrorCheck(int.Parse(result));
            
        }
        public string ConvertFileToString(IFormFile file)
        {
            string s = "";
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                bytes = ms.ToArray();
                s = Convert.ToBase64String(bytes);
            }
            return s;
        }
        public List<HealthRecorderRecordModel> ViewRecord(string username, int lastRecordIndex)
        {
           List<HealthRecorderRecordModel> records =  _HealthRecorderDAO.GetRecords(username, lastRecordIndex);
            if (records.Count == 0)
            {
                return new List<HealthRecorderRecordModel>();
            }
            else
            {
                string errorCode = records[0].ErrorCode;

                if (errorCode == null)
                {
                    return records;
                }
                records[0].ErrorCode = _dbError.DBErrorCheck(int.Parse((errorCode)));
                return records;
            }
        }
        public HealthRecorderResponseModel ValidateRecordExists(HealthRecorderRequestModel request, HealthRecorderResponseModel response, string username)
        {
           response = _HealthRecorderDAO.ValidateRecordExists(request, response, username);

            if(response.ErrorMessage == null)
            {
                return response;
            }
            else
            {
                response.ErrorMessage = _dbError.DBErrorCheck(int.Parse(response.ErrorMessage));
                return response;

            }
        }
        public HealthRecorderResponseModel DeleteRecord(HealthRecorderRequestModel request, HealthRecorderResponseModel response, string username)
        {
            response = _HealthRecorderDAO.DeleteRecord(request, response, username);

            if (response.ErrorMessage == null)
            {
                return response;
            }
            else
            {
                response.ErrorMessage = _dbError.DBErrorCheck(int.Parse(response.ErrorMessage));
                return response;

            }
        }
        public HealthRecorderViewRecordModel SearchRecord(HealthRecorderRequestModel request, HealthRecorderViewRecordModel response, string username)
        {
            response = _HealthRecorderDAO.SearchRecord(request, response, username);

            if (response.ErrorMessage == null)
            {
                return response;
            }
            else
            {
                response.ErrorMessage = _dbError.DBErrorCheck(int.Parse(response.ErrorMessage));
                return response;
            }
        }
        public HealthRecorderExportModel GetRecordByte(HealthRecorderRequestModel request, HealthRecorderExportModel response, string username)
        {
            response = _HealthRecorderDAO.ExportRecord(request, username, response);

            if (response.ErrorMessage == null)
            {
                return response;
            }
            else
            {
                response.ErrorMessage = _dbError.DBErrorCheck(int.Parse(response.ErrorMessage));
                return response;
            }
        }

    }

}
