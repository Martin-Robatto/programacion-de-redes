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
    public class PublishService
    {
        private static PublishService _instance;
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

        private PublishService() { }

        public string Get(string userLine)
        {
            IEnumerable<Publish> userPublishes = PublishRepository.GetAll(p => p.User.Username.Equals(userLine));
            if (!userPublishes.Any())
            {
                throw new NotFoundException("Publishs");
            }
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
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Save(attributes[1]);
            Publish input = new Publish()
            {
                Id = Guid.NewGuid(),
                User = user,
                Game = game,
                Date = DateTime.Now
            };
            PublishRepository.Add(input);
            string purchaseLine = $"{user.Username}&{game.Title}";
            PurchaseService.Instance.Save(purchaseLine);
            Console.WriteLine($"Usuario {user.Username} publico el juego {game.Title}");
        }

        public void Delete(string publishLine)
        {
            string[] attributes = publishLine.Split("&");
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            var publish = PublishRepository.Get(p => p.Game.Equals(game) && p.User.Equals(user));
            if (publish is null)
            {
                throw new NotFoundException("Purchase");
            }

            IList<Review> reviews = ReviewService.Instance.GetAll(r => r.Game.Equals(game)).ToList();
            foreach (var review in reviews)
            {
                ReviewService.Instance.Delete(review);
            }

            IList<Purchase> purchases = PurchaseService.Instance.GetAll(p => p.Game.Equals(game)).ToList();
            foreach (var purchase in purchases)
            {
                PurchaseService.Instance.Delete(purchase);
            }
            GameService.Instance.Delete(game);
            PublishRepository.Remove(publish);
            Console.WriteLine($"Usuario {user.Username} elimino el juego {game.Title}");
        }
    }
}
