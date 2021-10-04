using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ReviewRepository
    {
        private static ReviewRepository _instance;
        private static readonly object _lock = new object();
        private IList<Review> _reviews;
        public static IList<Review> Reviews
        {
            get { return GetInstance()._reviews; }
        }

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

        public static IEnumerable<Review> GetAll(Func<Review, bool> filter = null)
        {
            lock (Reviews)
            {
                IEnumerable<Review> reviewsToReturn = Reviews;
                if (filter is not null)
                {
                    reviewsToReturn = reviewsToReturn.Where(filter);
                }
                return reviewsToReturn;
            }
        }

        public static Review Get(Func<Review, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }

        public static void Add(Review review)
        {
            lock (Reviews)
            {
                Reviews.Add(review);
            }
        }

        public static void Remove(Review review)
        {
            lock (Reviews)
            {
                Reviews.Remove(review);
            }
        }

        public static void Update(Review review)
        {
            lock (Reviews)
            {
                Review reviewToUpdate = Reviews.FirstOrDefault(r => r.Equals(review));
                reviewToUpdate.Date = review.Date;
                reviewToUpdate.Rate = review.Rate;
                reviewToUpdate.Comment = review.Comment;
            }
        }
    }
}
