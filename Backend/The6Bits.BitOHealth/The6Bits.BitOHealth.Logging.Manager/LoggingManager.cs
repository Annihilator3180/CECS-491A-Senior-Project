using System;
using The6Bits.Logging.Implementations;

namespace The6Bits.Logging.Manager
{
    public class LoggingManager
    {
        
        //TODO : SWITCH FROM ENUM TO EITHER CLASS OR STRING
        public bool SendLog(string username, string description,Enum LogLevel, Enum LogCategory)
        {
            SQLLogService logger = new SQLLogService();
            logger.Log(username,description,LogLevel,LogCategory);
            return true;
        }
    }
}