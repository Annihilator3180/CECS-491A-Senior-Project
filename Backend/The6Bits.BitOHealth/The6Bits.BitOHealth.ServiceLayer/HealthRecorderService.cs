﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;
using The6Bits.DBErrors;

namespace The6Bits.BitOHealth.ServiceLayer
{
    public class HealthRecorderService
    {
        private IRepositoryHealthRecorderDAO _HealthRecorderDao;
        private IDBErrors _dbError;

        public HealthRecorderService(IRepositoryHealthRecorderDAO dao, IDBErrors dBErrors)
        {
            _HealthRecorderDao = dao;
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
        public string ValidateUserRecordLimit(string username) 
        {
            string result = _HealthRecorderDao.ValidateUserRecordLimits(username);
            
            if (result == "over record limit" || result == "over daily limit" || result == "under limit")
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
        public string SaveRecord(IFormFile file, DateTime now, string username, string categoryName, string recordName, IFormFile? file2 = null)
        {
            //add string to separate 2 records and parse later
            string conversion = ConvertFileToString(file);
            if (file2 != null)
            {
                string secondConversion = ConvertFileToString(file2);
                string temp = conversion + " *****SecondFile***** " + secondConversion;
                conversion = temp;
            }
            string result = _HealthRecorderDao.SaveRecord(conversion, now, username, categoryName, recordName);
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

    }

}