using DataAccess;
using Domain;
using System;
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
    }
}
