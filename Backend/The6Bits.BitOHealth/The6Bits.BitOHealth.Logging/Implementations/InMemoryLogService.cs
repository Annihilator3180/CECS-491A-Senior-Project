using System;
using System.Collections.Generic;
using The6Bits.Logging.DAL.Implementations;

namespace The6Bits.Logging.Implementations
{
    public class InMemoryLogService : ILogService
    {

        ILogDal sqlDAO = new InMemoryLogDAO();


        private readonly IList<string> _logStore;

        public InMemoryLogService() 
        {
            _logStore = new List<string>();
        }



        public bool Log(string username, string description, string LogLevel, string LogCategory)
        {
            try
            {
                _logStore.Add($"{LogLevel} {username} {LogCategory} {DateTime.UtcNow} {description}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public String getAllLogs()
        {
            String allLogs = "";
            foreach (var i in _logStore) { 
                allLogs += i.ToString() + " ";
            }
            return allLogs;
        }

    }
}