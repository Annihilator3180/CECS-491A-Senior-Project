namespace The6Bits.Logging.Models
{
    public class Log
    {
        public string username { get; set; }
        public string description { get; set; }

        public string LogLevel { get; set; }

        public string LogCategory { get; set; }

        public DateTime Date_Time { get; set; }

    }
}