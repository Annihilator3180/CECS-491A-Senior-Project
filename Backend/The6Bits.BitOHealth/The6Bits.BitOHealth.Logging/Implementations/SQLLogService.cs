 using System;
using System.Data.SqlClient;
using Dapper;
using The6Bits.Logging.DAL;

namespace The6Bits.Logging.Implementations
{
    public class SQLLogService : ILogService
    {
        SQLLogDAO sqlDAO = new SQLLogDAO();
        public string getAllLogs()
        {
            return sqlDAO.getAllLogs();
        }

        public bool Log(string username, string description, string LogLevel, string LogCategory)
        {
            return sqlDAO.Log(username, description, LogLevel, LogCategory);
        }
    }
}
