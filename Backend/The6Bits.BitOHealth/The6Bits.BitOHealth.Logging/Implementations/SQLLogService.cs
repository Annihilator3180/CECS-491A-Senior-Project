 using System;
using System.Data.SqlClient;
using Dapper;
using The6Bits.Logging.DAL.Implementations;

namespace The6Bits.Logging.Implementations
{
    public class SQLLogService : ILogService
    {
        private ILogDal sqlDAO;

        public SQLLogService(ILogDal loggerType)
        {

            sqlDAO = new SQLLogDAO();
        }

        public string getAllLogs()
        {
            return sqlDAO.getAllLogs();
        }
        public bool SyncLog(string username, string description, string LogLevel, string LogCategory)
        {
            return sqlDAO.Log(username, description, LogLevel, LogCategory);
        }
        public async Task<bool> Log(string username, string description, string LogLevel, string LogCategory)
        {
            return sqlDAO.Log(username, description, LogLevel, LogCategory);
        }
    }
}
