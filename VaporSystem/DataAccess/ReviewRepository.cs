using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ReviewRepository
    {
        private static ReviewRepository _instance;
        public static ReviewRepository Instance
        {
            get { return GetInstance(); }
        }
        
        private static readonly object _lock = new object();
        private static IList<Review> _reviews;

        private ReviewRepository()
        {
            _reviews = new List<Review>();
        }

        private static ReviewRepository GetInstance()
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new ReviewRepository();
                }
            }
            return _instance;
        }

        public IEnumerable<Review> GetAll(Func<Review, bool> filter = null)
        {
            lock (_reviews)
            {
                IEnumerable<Review> reviewsToReturn = _reviews;
                if (filter is not null)
                {
                    reviewsToReturn = reviewsToReturn.Where(filter);
                }
                return reviewsToReturn;
            }
        }

        public Review Get(Func<Review, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }

        public void Add(Review review)
        {
            lock (_reviews)
            {
                _reviews.Add(review);
            }
        }

        public void Remove(Review review)
        {
            lock (_reviews)
            {
                _reviews.Remove(review);
            }
        }
        
        public void Update(Review review)
        {
            lock (_reviews)
            {
                Review reviewToUpdate = _reviews.FirstOrDefault(r => r.Equals(review));
                reviewToUpdate.Date = review.Date;
                reviewToUpdate.Rate = review.Rate;
                reviewToUpdate.Comment = review.Comment;
            }
        }
    }
}
