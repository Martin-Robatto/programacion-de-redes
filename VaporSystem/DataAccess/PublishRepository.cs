using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class PublishRepository
    {
        private static PublishRepository _instance;
        public static PublishRepository Instance
        {
            get { return GetInstance(); }
        }
        
        private static readonly object _lock = new object();
        private static IList<Publish> _publishs;

        private PublishRepository()
        {
            _publishs = new List<Publish>();
        }

        private static PublishRepository GetInstance()
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new PublishRepository();
                }
            }
            return _instance;
        }

        public IEnumerable<Publish> GetAll(Func<Publish, bool> filter = null)
        {
            lock (_publishs)
            {
                IEnumerable<Publish> publishsToReturn = _publishs;
                if (filter is not null)
                {
                    publishsToReturn = publishsToReturn.Where(filter);
                }
                return publishsToReturn;
            }
        }

        public Publish Get(Func<Publish, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }

        public void Add(Publish publish)
        {
            lock (_publishs)
            {
                _publishs.Add(publish);
            }
        }

        public void Remove(Publish publish)
        {
            lock (_publishs)
            {
                _publishs.Remove(publish);
            }
        }
    }
}
