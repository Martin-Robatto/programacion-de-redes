using Domain;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class UserRepository
    {
        private static UserRepository _instance;
        private IList<User> _users;

        private UserRepository()
        {
            _users = new List<User>();
        }

        public static IList<User> Get()
        {
            return GetInstance()._users;
        }

        private static UserRepository GetInstance()
        {
            if (_instance is null)
            {
                _instance = new UserRepository();
            }
            return _instance;
        }
    }
}
