using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain;
using Exceptions;

namespace Service
{
    public class ReviewValidator
    {
        public void CheckReviewIsNull(Review review)
        {
            if (review is null)
            {
                throw new NotFoundException("Review");
            }
        }
        
        public void CheckReviewsAreEmpty(IEnumerable<Review> userReviews)
        {
            if (!userReviews.Any())
            {
                throw new NotFoundException("Reviews");
            }
        }
        
        public void CheckReviewAlreadyExists(Review input)
        {
            var review = ReviewRepository.Get(r => r.Equals(input));
            if (review is not null)
            {
                throw new AlreadyExistsException("Review");
            }
        }
    }
}