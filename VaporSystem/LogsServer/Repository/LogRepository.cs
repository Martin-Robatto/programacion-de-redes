using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace LogsServer.Repository
{
    public class LogRepository
    {
        private static LogRepository _instance;
        public static LogRepository Instance
        {
            get { return GetInstance(); }
        }
        
        private static readonly object _lock = new object();
        private static IList<Log> _logs;

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
        
        public IEnumerable<Log> GetAll(Func<Log, bool> filter = null)
        {
            lock (_logs)
            {
                IEnumerable<Log> logsToReturn = _logs;
                if (filter is not null)
                {
                    logsToReturn = logsToReturn.Where(filter);
                }
                return logsToReturn;
            }
        }
        
        public void Add(Log log)
        {
            lock (_logs)
            {
                _logs.Add(log);
            }
        }
    }
}