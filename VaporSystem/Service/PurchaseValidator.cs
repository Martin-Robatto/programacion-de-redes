using DataAccess;
using Domain;
using Exceptions;
using System.Collections.Generic;
using System.Linq;

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

        public void CheckAttributesAreEmpty(string[] attributes)
        {
            foreach (var attribute in attributes)
            {
                if (string.IsNullOrEmpty(attribute))
                {
                    throw new InvalidInputException("empty attribute");
                }
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