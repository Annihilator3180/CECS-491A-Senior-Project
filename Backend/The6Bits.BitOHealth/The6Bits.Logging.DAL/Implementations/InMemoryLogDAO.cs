using System;
using System.Collections.Generic;

namespace The6Bits.Logging.DAL.Implementations
{
    public class InMemoryLogDAO : ILogDal
    {



        private readonly IList<string> _logStore;

        public InMemoryLogDAO() 
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