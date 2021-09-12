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

        public void Register(string userLine)
        {
            string[] userAttributes = userLine.Split("#");
            User user = new User()
            {
                Email = userAttributes[0],
                Username = userAttributes[1],
                Password = userAttributes[2]
            };
            UserRepository.Get().Add(user);
            Console.WriteLine($"New user: {user.Username}");
        }
        
    }
}
