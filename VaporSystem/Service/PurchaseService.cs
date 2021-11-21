using DataAccess;
using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public class PurchaseService
    {
        private static PurchaseService _instance;
        private PurchaseValidator _validator;

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

        private PurchaseService()
        {
            _validator = new PurchaseValidator();
        }

        public IEnumerable<Purchase> GetAll(Func<Purchase, bool> filter = null)
        {
            return PurchaseRepository.Instance.GetAll(filter);
        }

        public string Get(string userLine)
        {
            IEnumerable<Purchase> userPurchases = PurchaseRepository.Instance.GetAll(p => p.User.Username.Equals(userLine));
            _validator.CheckPurchasesAreEmpty(userPurchases);
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
            _validator.CheckAttributesAreEmpty(attributes);
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            Purchase input = new Purchase()
            {
                Id = Guid.NewGuid(),
                User = user,
                Game = game,
                Date = DateTime.Now
            };
            _validator.CheckPurchaseAlreadyExists(input);
            PurchaseRepository.Instance.Add(input);
            Console.WriteLine($"{user.Username} compro: {game.Title}");
        }

        public void Delete(Purchase purchase)
        {
            User user = purchase.User;
            Game game = purchase.Game;
            var aPurchase = PurchaseRepository.Instance.Get(p => p.Equals(purchase));
            _validator.CheckPurchaseIsNull(aPurchase);
            PurchaseRepository.Instance.Remove(aPurchase);
        }

        public void Delete(string purchaseLine)
        {
            string[] attributes = purchaseLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            User user = UserService.Instance.Get(attributes[0]);
            Game game = GameService.Instance.Get(attributes[1]);
            var purchase = PurchaseRepository.Instance.Get(p => p.Game.Equals(game) && p.User.Equals(user));
            _validator.CheckPurchaseIsNull(purchase);
            PurchaseRepository.Instance.Remove(purchase);
            Console.WriteLine($"{user.Username} desinstalo: {game.Title}");
        }
    }
}