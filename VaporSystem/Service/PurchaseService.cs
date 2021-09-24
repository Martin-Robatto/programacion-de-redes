using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain;
using Exceptions;

namespace Service
{
    public class PurchaseService
    {
        private static PurchaseService _instance;
        public static PurchaseService Instance
        {
            get { return GetInstance(); }
        }

        private static PurchaseService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new PurchaseService();
            }
            return _instance;
        }

        private PurchaseService() { }

        public string Get(string userLine)
        {
            IEnumerable<Purchase> userPurchases = PurchaseRepository.GetAll(p => p.User.Username.Equals(userLine));
            if (!userPurchases.Any())
            {
                throw new NotFoundException("Purchases");
            }
            string purchasesLine = string.Empty;
            foreach (Purchase purchase in userPurchases)
            {
                purchasesLine += "#" + purchase.Game.Title;
            }
            return purchasesLine;
        }

        public void Save(string purchaseLine)
        {
            string[] attributes = purchaseLine.Split("&");
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            Purchase input = new Purchase()
            {
                Id = Guid.NewGuid(),
                User = user,
                Game = game,
                Date = DateTime.Now
            };
            var purchase = PurchaseRepository.Get(p => p.Equals(input));
            if (purchase is not null)
            {
                throw new AlreadyExistsException("Purchase");
            }
            PurchaseRepository.Add(input);
            Console.WriteLine($"Usuario {user.Username} compro el juego {game.Title}");
        }

        public void Delete(string purchaseLine)
        {
            string[] attributes = purchaseLine.Split("&");
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            var purchase = PurchaseRepository.Get(p => p.Game.Equals(game) && p.User.Equals(user));
            if (purchase is null)
            {
                throw new NotFoundException("Purchase");
            }
            PurchaseRepository.Remove(purchase);
            Console.WriteLine($"Usuario {user.Username} desinstaló el juego {game.Title}");
        }
    }
}