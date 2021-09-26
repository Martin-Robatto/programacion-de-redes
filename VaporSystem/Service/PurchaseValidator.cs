using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain;
using Exceptions;

namespace Service
{
    public class PurchaseValidator
    {
        public void CheckPurchaseIsNull(Purchase purchase)
        {
            if (purchase is null)
            {
                throw new NotFoundException("Purchase");
            }
        }
        
        public void CheckPurchasesAreEmpty(IEnumerable<Purchase> purchases)
        {
            if (!purchases.Any())
            {
                throw new NotFoundException("Purchases");
            }
        }
        
        public void CheckPurchaseAlreadyExists(Purchase input)
        {
            var purchase = PurchaseRepository.Get(p => p.Equals(input));
            if (purchase is not null)
            {
                throw new AlreadyExistsException("Purchase");
            }
        }
    }
}