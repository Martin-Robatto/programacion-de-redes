using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class PublishRepository
    {
        private static PublishRepository _instance;
        private static readonly object _lock = new object();
        private IList<Publish> _publishs;
        public static IList<Publish> Publishs
        {
            get { return GetInstance()._publishs; }
        }

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

        public static IEnumerable<Publish> GetAll(Func<Publish, bool> filter = null)
        {
            IEnumerable<Publish> publishsToReturn = Publishs;
            if (filter is not null)
            {
                publishsToReturn = publishsToReturn.Where(filter);
            }
            return publishsToReturn;
        }
        
        public static Publish Get(Func<Publish, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }
        
        public static void Add(Publish publish)
        {
            lock (Publishs)
            {
                Publishs.Add(publish);
            }
        }
        
        public static void Remove(Publish publish)
        {
            lock (Publishs)
            {
                Publishs.Remove(publish);
            }
        }
    }
}
