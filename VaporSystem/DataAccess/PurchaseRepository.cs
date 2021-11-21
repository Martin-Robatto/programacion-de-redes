using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class PurchaseRepository
    {
        private static PurchaseRepository _instance;
        public static PurchaseRepository Instance
        {
            get { return GetInstance(); }
        }
        
        private static readonly object _lock = new object();
        private static IList<Purchase> _purchases;

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

        public IEnumerable<Purchase> GetAll(Func<Purchase, bool> filter = null)
        {
            lock (_purchases)
            {
                IEnumerable<Purchase> purchasesToReturn = _purchases;
                if (filter is not null)
                {
                    purchasesToReturn = purchasesToReturn.Where(filter);
                }
                return purchasesToReturn;
            }
        }

        public Purchase Get(Func<Purchase, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }

        public void Add(Purchase purchase)
        {
            lock (_purchases)
            {
                _purchases.Add(purchase);
            }
        }

        public void Remove(Purchase purchase)
        {
            lock (_purchases)
            {
                _purchases.Remove(purchase);
            }
        }
    }
}