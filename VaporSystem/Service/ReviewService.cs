using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using Exceptions;

namespace Service
{
    public class ReviewService
    {
        private static ReviewService _instance;
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

        private ReviewService() { }


        public string Get(string userLine)
        {
            string reviews = string.Empty;
            IEnumerable<Review> userReviews = ReviewRepository.Get().Where(review => review.User.Username.Equals(userLine));
            foreach (Review review in userReviews)
            {
                reviews += "#" + $"{review.Game.Title} [{review.Rate}]: {review.Comment}";
            }
            if (userReviews.Count() == 0)
            {
                throw new NotFoundException("Reviews");
            }
            return reviews;
        }

        public void Save(string reviewLine)
        {
            string[] attributes = reviewLine.Split("&");
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            string[] reviewAttributes = attributes[2].Split("#");
            int rate = Int32.Parse(reviewAttributes[0]);
            Review input = new Review()
            {
                Id = Guid.NewGuid(),
                User = user,
                Game = game,
                Date = DateTime.Now,
                Rate = rate,
                Comment = reviewAttributes[1]
            };
            var review = ReviewRepository.Get().FirstOrDefault(review => review.Game.Title.Equals(input.Game.Title)
                                                                               && review.User.Username.Equals(input.User.Username));
            if (review is not null)
            {
                throw new AlreadyExistsException("Review");
            }
            ReviewRepository.Get().Add(input);
            game.Rate = CalculateMediaRate(game);
            Console.WriteLine($"Usuario {user.Username} califico el juego {game.Title}");
        }

        private float CalculateMediaRate(Game game)
        {
            var reviews = ReviewRepository.Get().Where(review => review.Game.Title.Equals(game.Title));
            float mediaRate = 0;
            if (reviews.Count() > 0)
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

        public void Delete(string reviewLine)
        {
            string[] attributes = reviewLine.Split("&");
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            var review = ReviewRepository.Get().FirstOrDefault(review => review.Game.Title.Equals(game.Title)
                                                                         && review.User.Username.Equals(user.Username));
            if (review is null)
            {
                throw new NotFoundException("Review");
            }
            ReviewRepository.Get().Remove(review);
            Console.WriteLine($"Usuario {user.Username} eliminó la calificación del juego {game.Title}");
            game.Rate = CalculateMediaRate(game);
        }
    }
}
