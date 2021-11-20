using DataAccess;
using Domain;
using Exceptions;

namespace Service
{
    public class UserValidator
    {
        public void CheckCredentials(User input)
        {
            var user = UserRepository.Instance.Get(u => u.Username.Equals(input.Username)
                                               && u.Password.Equals(input.Password));
            if (user is null)
            {
                throw new InvalidInputException("username or password");
            }
        }

        public void CheckUserIsNull(User user)
        {
            if (user is null)
            {
                throw new NotFoundException("User");
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

        public void CheckUserAlreadyExists(User input)
        {
            var user = UserRepository.Instance.Get(u => u.Equals(input));
            if (user is not null)
            {
                throw new AlreadyExistsException("User");
            }
        }
    }
}