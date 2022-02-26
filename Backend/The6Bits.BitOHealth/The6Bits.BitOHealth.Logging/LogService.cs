 using System;
using System.Data.SqlClient;
using Dapper;
 using The6Bits.Logging.DAL.Contracts;
 using The6Bits.Logging.DAL.Implementations;

namespace The6Bits.Logging.Implementations
{
    public class LogService 
    {
        private ILogDal _dao;

        public LogService(ILogDal loggerType)
        {

            _dao = loggerType;
        }

        public string getAllLogs()
        {
            return _dao.getAllLogs();
        }
        
        public async Task<bool> Log(string username, string description, string LogLevel, string LogCategory)
        {
            return _dao.Log(username, description, LogLevel, LogCategory);
        }

        public async Task<bool> RegistrationLog(string username, string description, string LogLevel, string LogCategory)
        {
            bool daoLog =  _dao.Log(username, description, LogLevel, LogCategory);

            if(daoLog == true)
            {
                bool checker1= _dao.RegistrationChecker(username, description, LogLevel, LogCategory);

            }

            else
            {
                //handle not being able to log
            }

            return daoLog;
        }
    }
}
