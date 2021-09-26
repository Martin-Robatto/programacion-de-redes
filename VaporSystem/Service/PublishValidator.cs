using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

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
    }
}