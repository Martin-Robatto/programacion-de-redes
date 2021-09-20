using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using Exceptions;

namespace Service
{
    public class UserService
    {
        private static UserService _instance;
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
        
        private UserService() { }

        public string Register(string userLine)
        {
            string[] userAttributes = userLine.Split("#");
            User inputUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = userAttributes[0],
                Password = userAttributes[1]
            };
            var user = UserRepository.Get().FirstOrDefault(user => user.Username.Equals(inputUser.Username));
            if (user is not null)
            {
                throw new AlreadyExistsException(inputUser.Username);
            }
            UserRepository.Get().Add(inputUser);
            Console.WriteLine($"Nuevo usuario: {inputUser.Username}");
            return inputUser.Username;
        }

        public string LogIn(string userLine)
        {
            string[] userAttributes = userLine.Split("#");
            User inputUser = new User()
            {
                Username = userAttributes[0],
                Password = userAttributes[1]
            };
            var user = UserRepository.Get().FirstOrDefault(user => user.Username.Equals(inputUser.Username) 
                                                             && user.Password.Equals(inputUser.Password));
            if (user is null)
            {
                throw new InvalidInputException("username or password");
            }
            Console.WriteLine($"Usuario conectado: {inputUser.Username}");
            return inputUser.Username;
        }

        public User Get(string username)
        {
            User user = UserRepository.Get().FirstOrDefault(user => user.Username.Equals(username));
            if (user is null)
            {
                throw new NotFoundException(username);
            }
            return user;
        }
    }
}
