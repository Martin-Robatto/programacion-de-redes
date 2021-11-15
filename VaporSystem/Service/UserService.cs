using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleServer;

namespace Service
{
    public class UserService
    {
        private static UserService _instance;
        private UserValidator _validator;

        public static UserService Instance
        {
            get { return GetInstance(); }
        }

        private static UserService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new UserService();
            }
            return _instance;
        }

        private UserService()
        {
            _validator = new UserValidator();
        }

        public string Register(string userLine)
        {
            string[] attributes = userLine.Split("#");
            _validator.CheckAttributesAreEmpty(attributes);
            User input = new User()
            {
                Id = Guid.NewGuid(),
                Username = attributes[0],
                Password = attributes[1]
            };
            _validator.CheckUserAlreadyExists(input);
            UserRepository.Add(input);
            Console.WriteLine($"Usuario nuevo: {input.Username}");
            return input.Username;
        }

        public string LogIn(string userLine)
        {
            string[] attributes = userLine.Split("#");
            _validator.CheckAttributesAreEmpty(attributes);
            User input = new User()
            {
                Username = attributes[0],
                Password = attributes[1]
            };
            _validator.CheckCredentials(input);
            Console.WriteLine($"Usuario conectado: {input.Username}");
            return input.Username;
        }

        public User Get(string username)
        {
            User user = UserRepository.Get(u => u.Username.Equals(username));
            _validator.CheckUserIsNull(user);
            return user;
        }

        public void Delete(string userLine)
        {
            string[] attributes = userLine.Split("#");
            _validator.CheckAttributesAreEmpty(attributes);
            User user = Get(attributes[0]);
            DeleteReviews(user);
            DeletePurchases(user);
            UserRepository.Remove(user);
        }
        
        private void DeleteReviews(User user)
        {
            IList<Review> reviews = ReviewService.Instance.GetAll(r => r.User.Equals(user)).ToList();
            foreach (var review in reviews)
            {
                ReviewService.Instance.Delete(review);
            }
        }

        private void DeletePurchases(User user)
        {
            IList<Purchase> purchases = PurchaseService.Instance.GetAll(p => p.User.Equals(user)).ToList();
            foreach (var purchase in purchases)
            {
                PurchaseService.Instance.Delete(purchase);
            }
        }

        public void Update(string userLine)
        {
            string[] attributes = userLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            string[] userAttributes = userLine.Split("#");
            _validator.CheckAttributesAreEmpty(userAttributes);
            User user = Get(attributes[0]);
            if (!user.Username.Equals(userAttributes[0]))
            {
                User anUser = new User()
                {
                    Username = userAttributes[0]
                };
                _validator.CheckUserAlreadyExists(anUser);
            }
            user.Username = userAttributes[0];
            user.Password = userAttributes[1];
            Console.WriteLine($"Usuario modificado: {user.Username}");
        }
    }
}
