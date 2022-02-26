using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.Logging.DAL.Contracts
{
    public interface ILogDal
    {

        bool Log(string username, string description, string LogLevel, string LogCategory);
        string getAllLogs();
        bool RegistrationChecker(string username, string description, string logLevel, string logCategory);
    }
}
