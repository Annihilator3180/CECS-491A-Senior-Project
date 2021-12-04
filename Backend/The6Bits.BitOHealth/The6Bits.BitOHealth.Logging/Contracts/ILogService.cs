using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.Logging
{
    public interface ILogService
    {

        Task<bool> Log(string username, string description, string LogLevel, string LogCategory);
        bool SyncLog(string username, string description, string LogLevel, string LogCategory);
        String getAllLogs();


    }
}
