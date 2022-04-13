using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.Models;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IRepositoryHealthRecorderDAO
    {
        public string ValidateUserRecordLimits(string username);

        public string SaveRecord(string record, DateTime now, string username, string categoryName, string recordName, string secondRecord);

        public List<HealthRecorderRecordModel> GetRecords(string username, int lastRecordIndex);

        public HealthRecorderResponseModel ValidateRecordExists(HealthRecorderRequestModel request, HealthRecorderResponseModel response, string username);
        public HealthRecorderResponseModel DeleteRecord(HealthRecorderRequestModel request, HealthRecorderResponseModel response, string username);
    }
}
