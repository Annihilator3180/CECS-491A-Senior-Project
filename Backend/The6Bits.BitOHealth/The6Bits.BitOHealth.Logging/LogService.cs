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
            bool log = _dao.Log(username, description, LogLevel, LogCategory);


            return _dao.Log(username, description, LogLevel, LogCategory);

        }

        public async Task<bool> RegistrationLog(string username, string description, string LogLevel, string LogCategory)
        {
            bool daoLog =  _dao.Log(username, description, LogLevel, LogCategory);
            
            if(daoLog == true)
            {
                bool checker = _dao.RegistrationChecker(username, description, LogLevel, LogCategory);

                //if true then table already has something for the given
                //date, so we update the table and add one

                if (checker == true)
                {
                    return _dao.RegistrationLog(username, description, LogLevel, LogCategory);

                }
                //if false then table doe not have something for the given
                //date, so we insert a new row to the table 
                else if (checker == false)
                {
                    return _dao.RegistrationInsert(username, description, LogLevel, LogCategory);

                }
            }

            return daoLog;
        }
    }
}
