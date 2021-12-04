﻿ using System;
using System.Data.SqlClient;
using Dapper;
using The6Bits.Logging.DAL.Implementations;

namespace The6Bits.Logging.Implementations
{
    public class SQLLogService : ILogService
    {
        ILogDal sqlDAO = new SQLLogDAO();
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
