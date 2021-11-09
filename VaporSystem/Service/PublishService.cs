using DataAccess;
using Domain;
using SocketLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using ConsoleServer;

namespace Service
{
    public class PublishService
    {
        private static PublishService _instance;
        private PublishValidator _validator;

        private NetworkManager _networkManager = new NetworkManager();

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
            IEnumerable<Publish> userPublishes = PublishRepository.GetAll(p => p.User.Username.Equals(userLine));
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
            Log newLog = new Log()
            {
                User = user.Username,
                Date = DateTime.Today.ToShortDateString(),
                Game = game.Title,
                Action = "Saved a publish"
            };
            LogSender.Instance.SendLog(newLog);
            PublishRepository.Add(input);
            string purchaseLine = $"{user.Username}&{game.Title}";
            PurchaseService.Instance.Save(purchaseLine);
        }

        public void Delete(string publishLine)
        {
            CheckInput(publishLine);
            string[] attributes = publishLine.Split("&");
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            var publish = PublishRepository.Get(p => p.User.Equals(user) && p.Game.Equals(game));
            DeleteReviews(game);
            DeletePurchases(game);
            GameService.Instance.Delete(game);
            PublishRepository.Remove(publish);
            Log newLog = new Log()
            {
                User = user.Username,
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                Game = game.Title,
                Action = "Deleted a publish"
            };
            LogSender.Instance.SendLog(newLog);
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
            var publish = PublishRepository.Get(p => p.User.Equals(user) && p.Game.Equals(game));
            _validator.CheckPublishIsNull(publish);
        }
    }
}
