using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ReviewService
    {
        private static ReviewService _instance;
        private ReviewValidator _validator;

        public static ReviewService Instance
        {
            get { return GetInstance(); }
        }

        private static ReviewService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new ReviewService();
            }
            return _instance;
        }

        private ReviewService()
        {
            _validator = new ReviewValidator();
        }

        public IEnumerable<Review> GetAll(Func<Review, bool> filter = null)
        {
            return ReviewRepository.Instance.GetAll(filter);
        }

        public string GetByUser(string userLine)
        {
            IEnumerable<Review> userReviews = ReviewRepository.Instance.GetAll(r => r.User.Username.Equals(userLine));
            _validator.CheckReviewsAreEmpty(userReviews);
            string reviewsLine = string.Empty;
            foreach (Review review in userReviews)
            {
                reviewsLine += "#" + $"{review.Game.Title} [{review.Rate}]: {review.Comment}";
            }
            return reviewsLine;
        }

        public string GetByGame(string gameLine)
        {
            string[] attributes = gameLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            IEnumerable<Review> userReviews = ReviewRepository.Instance.GetAll(r => r.Game.Title.Equals(attributes[1]));
            _validator.CheckReviewsAreEmpty(userReviews);
            string reviewsLine = string.Empty;
            foreach (Review review in userReviews)
            {
                reviewsLine += "#" + $"{review.Game.Title} [{review.Rate}]: {review.Comment}";
            }
            return reviewsLine;
        }

        public void Save(string reviewLine)
        {
            string[] attributes = reviewLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            string[] reviewAttributes = attributes[2].Split("#");
            _validator.CheckAttributesAreEmpty(reviewAttributes);
            int rate = Int32.Parse(reviewAttributes[0]);
            _validator.CheckRateIsInRange(rate);
            Review input = new Review()
            {
                Id = Guid.NewGuid(),
                User = user,
                Game = game,
                Date = DateTime.Now,
                Rate = rate,
                Comment = reviewAttributes[1]
            };
            _validator.CheckReviewAlreadyExists(input);
            ReviewRepository.Instance.Add(input);
            game.Rate = CalculateMediaRate(game);
            Console.WriteLine($"{user.Username} califico: {game.Title}");
        }

        public void Delete(Review review)
        {
            User user = review.User;
            Game game = review.Game;
            var aReview = ReviewRepository.Instance.Get(r => r.Equals(review));
            _validator.CheckReviewIsNull(aReview);
            ReviewRepository.Instance.Remove(aReview);
            game.Rate = CalculateMediaRate(game);
        }

        public void Delete(string reviewLine)
        {
            string[] attributes = reviewLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            var review = ReviewRepository.Instance.Get(r => r.Game.Equals(game) && r.User.Equals(user));
            _validator.CheckReviewIsNull(review);
            ReviewRepository.Instance.Remove(review);
            game.Rate = CalculateMediaRate(game);
            Console.WriteLine($"{user.Username} descalifico: {game.Title}");
        }

        private float CalculateMediaRate(Game game)
        {
            var reviews = ReviewRepository.Instance.GetAll(r => r.Game.Equals(game));
            float mediaRate = 0;
            if (reviews.Any())
            {
                int total = 0;
                foreach (var review in reviews)
                {
                    total += review.Rate;
                }
                mediaRate = total / reviews.Count();
            }
            return mediaRate;
        }

        public void Update(string reviewLine)
        {
            string[] attributes = reviewLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            string[] reviewAttributes = attributes[2].Split("#");
            _validator.CheckAttributesAreEmpty(reviewAttributes);
            int rate = Int32.Parse(reviewAttributes[0]);
            _validator.CheckRateIsInRange(rate);
            string comment = reviewAttributes[1];
            Review input = new Review()
            {
                User = user,
                Game = game,
                Rate = rate,
                Comment = comment
            };
            var review = ReviewRepository.Instance.Get(r => r.Equals(input));
            _validator.CheckReviewIsNull(review);
            ReviewRepository.Instance.Update(input);
            game.Rate = CalculateMediaRate(game);
            Console.WriteLine($"{user.Username} califico: {game.Title}");
        }
    }
}
