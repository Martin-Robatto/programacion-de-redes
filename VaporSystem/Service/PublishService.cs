using DataAccess;
using Domain;
using SocketLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class PublishService
    {
        private static PublishService _instance;
        private PublishValidator _validator;
        public static PublishService Instance
        {
            get { return GetInstance(); }
        }

        private static PublishService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new PublishService();
            }
            return _instance;
        }

        private PublishService()
        {
            _validator = new PublishValidator();
        }

        public string Get(string userLine)
        {
            IEnumerable<Publish> userPublishes = PublishRepository.Instance.GetAll(p => p.User.Username.Equals(userLine));
            _validator.CheckPublishesAreEmpty(userPublishes);
            string publishsLine = string.Empty;
            foreach (Publish publish in userPublishes)
            {
                publishsLine += "#" + publish.Game.Title;
            }
            return publishsLine;
        }

        public void Save(string publishLine)
        {
            string[] attributes = publishLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Save(attributes[1]);
            Publish input = new Publish()
            {
                Id = Guid.NewGuid(),
                User = user,
                Game = game,
                Date = DateTime.Now
            };
            PublishRepository.Instance.Add(input);
            string purchaseLine = $"{user.Username}&{game.Title}";
            PurchaseService.Instance.Save(purchaseLine);
        }

        public void Delete(string publishLine)
        {
            CheckInput(publishLine);
            string[] attributes = publishLine.Split("&");
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            var publish = PublishRepository.Instance.Get(p => p.User.Equals(user) && p.Game.Equals(game));
            DeleteReviews(game);
            DeletePurchases(game);
            GameService.Instance.Delete(game);
            PublishRepository.Instance.Remove(publish);
        }

        private void DeleteReviews(Game game)
        {
            IList<Review> reviews = ReviewService.Instance.GetAll(r => r.Game.Equals(game)).ToList();
            foreach (var review in reviews)
            {
                ReviewService.Instance.Delete(review);
            }
        }

        private void DeletePurchases(Game game)
        {
            IList<Purchase> purchases = PurchaseService.Instance.GetAll(p => p.Game.Equals(game)).ToList();
            foreach (var purchase in purchases)
            {
                PurchaseService.Instance.Delete(purchase);
            }
        }

        public void Update(string publishLine)
        {
            CheckInput(publishLine);
            GameService.Instance.Update(publishLine);
        }

        public void CheckInput(string publishLine)
        {
            string[] attributes = publishLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            var publish = PublishRepository.Instance.Get(p => p.User.Equals(user) && p.Game.Equals(game));
            _validator.CheckPublishIsNull(publish);
        }
    }
}
