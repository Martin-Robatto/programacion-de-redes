using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Domain;

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
                Email = userAttributes[0],
                Username = userAttributes[1],
                Password = userAttributes[2]
            };
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
                throw new InvalidCastException(inputUser.Username);
            }
            Console.WriteLine($"Usuario conectado: {inputUser.Username}");
            return inputUser.Username;
        }
    }
}
