using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.Logging
{
    public interface ILogDal
    {

        bool Log(string username, string description, string LogLevel, string LogCategory);
        String getAllLogs();


    }
}
