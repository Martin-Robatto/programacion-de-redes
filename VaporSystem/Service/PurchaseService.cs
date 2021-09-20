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
            string purchases = string.Empty;
            IEnumerable<Purchase> userPurchases = PurchaseRepository.Get().Where(purchase => purchase.User.Username.Equals(userLine));
            foreach (Purchase purchase in userPurchases)
            {
                purchases += "#" + purchase.Game.Title;
            }
            if (userPurchases.Count() == 0)
            {
                throw new NotFoundException("Purchases");
            }
            return purchases;
        }

        public void Save(string purchaseLine)
        {
            string[] purchaseAttributes = purchaseLine.Split("&");
            User user = UserService.Instance.Get(purchaseAttributes[0]);
            Game game = GameService.Instance.Get(purchaseAttributes[1]);
            Purchase inputPurchase = new Purchase()
            {
                Id = Guid.NewGuid(),
                User = user,
                Game = game,
                Date = DateTime.Now
            };
            var purchase = PurchaseRepository.Get().FirstOrDefault(purchase => purchase.Game.Title.Equals(inputPurchase.Game.Title)
                                                                                && purchase.User.Username.Equals(inputPurchase.User.Username));
            if (purchase is not null)
            {
                throw new AlreadyExistsException("Purchase");
            }
            PurchaseRepository.Get().Add(inputPurchase);
            Console.WriteLine($"Usuario {user.Username} compro el juego {game.Title}");
        }
    }
}