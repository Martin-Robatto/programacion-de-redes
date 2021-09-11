using Domain;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class PublishRepository
    {
        private static PublishRepository _instance;
        private IList<Publish> _publishs;

        private PublishRepository()
        {
            _publishs = new List<Publish>();
        }

        public static IList<Publish> Get()
        {
            return GetInstance()._publishs;
        }

        private static PublishRepository GetInstance()
        {
            if (_instance is null)
            {
                _instance = new PublishRepository();
            }
            return _instance;
        }
    }
}
