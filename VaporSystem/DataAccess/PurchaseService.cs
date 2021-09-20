using System.Collections.Generic;
using Domain;

namespace DataAccess
{
    public class PurchaseService
    {
        private static PurchaseService _instance;
        private IList<Purchase> _purchases;

        private PurchaseService()
        {
            _purchases = new List<Purchase>();
        }

        public static IList<Purchase> Get()
        {
            return GetInstance()._purchases;
        }

        private static PurchaseService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new PurchaseService();
            }
            return _instance;
        }
    }
}