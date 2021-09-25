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
            string[] attributes = userLine.Split("#");
            User input = new User()
            {
                Id = Guid.NewGuid(),
                Username = attributes[0],
                Password = attributes[1]
            };
            var user = UserRepository.Get(u => u.Equals(input));
            if (user is not null)
            {
                throw new AlreadyExistsException("User");
            }
            UserRepository.Add(input);
            Console.WriteLine($"Usuario nuevo: {input.Username}");
            return input.Username;
        }

        public string LogIn(string userLine)
        {
            string[] attributes = userLine.Split("#");
            User input = new User()
            {
                Username = attributes[0],
                Password = attributes[1]
            };
            var user = UserRepository.Get(u => u.Username.Equals(input.Username) 
                                                  && u.Password.Equals(input.Password));
            if (user is null)
            {
                throw new InvalidInputException("username or password");
            }
            Console.WriteLine($"Usuario conectado: {input.Username}");
            return input.Username;
        }

        public User Get(string username)
        {
            User user = UserRepository.Get(u => u.Username.Equals(username));
            if (user is null)
            {
                throw new NotFoundException("User");
            }
            return user;
        }
    }
}
