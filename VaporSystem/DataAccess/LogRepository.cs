using System.Collections.Generic;
using Domain;

namespace DataAccess
{
    public class LogRepository
    {
        private static LogRepository _instance;
        private static readonly object _lock = new object();
        private IList<Log> _logs;

        public static IList<Log> Logs
        {
            get { return GetInstance()._logs; }
        }

        private LogRepository()
        {
            _logs = new List<Log>();
        }

        private static LogRepository GetInstance()
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new LogRepository();
                }
            }

            return _instance;
        }
        
        public static void Add(Log log)
        {
            lock (Logs)
            {
                Logs.Add(log);
            }
        }
    }
}