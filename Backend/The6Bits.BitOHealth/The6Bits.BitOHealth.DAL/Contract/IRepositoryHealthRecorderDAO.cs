using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.DAL.Contract
{
    public interface IRepositoryHealthRecorderDAO
    {
        public string ValidateUserRecordLimits(string username);

        public string SaveRecord(string record, DateTime now, string username, string categoryName, string recordName);
    }
}
