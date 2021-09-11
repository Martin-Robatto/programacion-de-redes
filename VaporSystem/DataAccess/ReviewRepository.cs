using Domain;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class ReviewRepository
    {
        private static ReviewRepository _instance;
        private IList<Review> _reviews;

        private ReviewRepository()
        {
            _reviews = new List<Review>();
        }

        public static IList<Review> Get()
        {
            return GetInstance()._reviews;
        }

        private static ReviewRepository GetInstance()
        {
            if (_instance is null)
            {
                _instance = new ReviewRepository();
            }
            return _instance;
        }
    }
}
