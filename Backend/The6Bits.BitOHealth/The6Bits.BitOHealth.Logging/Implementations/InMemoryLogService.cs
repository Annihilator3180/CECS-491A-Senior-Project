namespace The6Bits.Logging.Implementations
{
    public class InMemoryLogService : ILogService
    {



        private IList<String> _logStore;

        public InMemoryLogService() 
        {
            _logStore = new List<String>();
        }



        public bool Log(string username, string description, Enum LogLevel, Enum LogCategory)
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