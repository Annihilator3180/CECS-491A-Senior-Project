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
    }
}
