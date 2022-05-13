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

        public string getAllTrackerLogs()
        {
            return _dao.getAllTrackerLogs();
        }


        public async Task<bool> Log(string username, string description, string LogLevel, string LogCategory)
        {
            bool holder = false;
            await Task.Run(() =>
            {
                holder = _dao.Log(username, description, LogLevel, LogCategory);
            });
            return holder;

        }

        public async Task<bool> RegistrationLog(string username, string description, string LogLevel, string LogCategory)
        {
            bool daoLog = _dao.Log(username, description, LogLevel, LogCategory);
            bool checker = false;
            checker = _dao.RegistrationChecker(username, description, LogLevel, LogCategory);

            //if false then table doe not have something for the given
            //date, so we insert a new row to the table
            if (checker == false)
            {
                return _dao.RegistrationInsert(username, description, LogLevel, LogCategory);

            }
            //if true then table already has something for the given
            //date, so we update the table and add one 
            else if (checker == true)
            {
                return _dao.RegistrationLog(username, description, LogLevel, LogCategory);

            }
            

            return checker;
        }

        public async Task<bool> searchTracker(string item,string Type)
        {
            try
            {

                bool drugAlreadySearched = _dao.AlreadySearched(item, Type);
                if (drugAlreadySearched)
                {
                    return _dao.IncreaseSearchCount(item, Type);

                }
                return _dao.AddSearchItem(item, Type);
            }
            catch (Exception ex)
            {
                _dao.Log("", "Search Tracker " + ex.Message, "Data Store", "Error");
                return false;
            }
        }

        public async Task<bool> LoginLog(string username, string description, string LogLevel, string LogCategory)
        {
            bool daoLog = false;
            bool checker = false;
            await Task.Run(() =>
           {
               daoLog = _dao.Log(username, description, LogLevel, LogCategory);
               checker = _dao.LoginChecker(username, description, LogLevel, LogCategory);

           });
            //login checker returns -1 instead of a proper number

            //if false then table doe not have something for the given
            //date, so we insert a new row to the table

            if (checker == false)
            {
                return _dao.LoginInsert(username, description, LogLevel, LogCategory);

            }
            //if true then table already has something for the given
            //date, so we update the table and add one
            else if (checker == true)
            {
                return _dao.LoginLog(username, description, LogLevel, LogCategory);

            }

            return _dao.LoginInsert(username, description, LogLevel, checker.ToString());

            //return checker;
        }

    }
}
