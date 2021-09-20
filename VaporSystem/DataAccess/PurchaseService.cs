using System.Collections.Generic;
using Domain;

namespace DataAccess
{
    public class PurchaseRepository
    {
        private static PurchaseRepository _instance;
        private IList<Purchase> _purchases;

        private PurchaseRepository()
        {
            _purchases = new List<Purchase>();
        }

        public static IList<Purchase> Get()
        {
            return GetInstance()._purchases;
        }

        private static PurchaseRepository GetInstance()
        {
            if (_instance is null)
            {
                _instance = new PurchaseRepository();
            }
            return _instance;
        }
    }
}