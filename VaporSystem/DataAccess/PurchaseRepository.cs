using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class PurchaseRepository
    {
        private static PurchaseRepository _instance;
        private static readonly object _lock = new object();
        private IList<Purchase> _purchases;
        public static IList<Purchase> Purchases
        {
            get { return GetInstance()._purchases; }
        }

        private PurchaseRepository()
        {
            _purchases = new List<Purchase>();
        }

        private static PurchaseRepository GetInstance()
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new PurchaseRepository();
                }
            }
            return _instance;
        }

        public static IEnumerable<Purchase> GetAll(Func<Purchase, bool> filter = null)
        {
            lock (Purchases)
            {
                IEnumerable<Purchase> purchasesToReturn = Purchases;
                if (filter is not null)
                {
                    purchasesToReturn = purchasesToReturn.Where(filter);
                }
                return purchasesToReturn;
            }
        }

        public static Purchase Get(Func<Purchase, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }

        public static void Add(Purchase purchase)
        {
            lock (Purchases)
            {
                Purchases.Add(purchase);
            }
        }

        public static void Remove(Purchase purchase)
        {
            lock (Purchases)
            {
                Purchases.Remove(purchase);
            }
        }
    }
}