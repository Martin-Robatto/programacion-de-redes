using Domain;
using Exceptions;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace Service
{
    public class PublishValidator
    {
        public void CheckPublishIsNull(Publish publish)
        {
            if (publish is null)
            {
                throw new NotFoundException("Publishs");
            }
        }

        public void CheckPublishesAreEmpty(IEnumerable<Publish> publishes)
        {
            if (!publishes.Any())
            {
                throw new NotFoundException("Publishs");
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
        
        public void CheckPublishAlreadyExists(Publish input)
        {
            var publish = PublishRepository.Instance.Get(p => p.Equals(input));
            if (publish is not null)
            {
                throw new AlreadyExistsException("Publish");
            }
        }
    }
}