using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class UserRepository
    {
        private static UserRepository _instance;
        private static readonly object _lock = new object();
        private IList<User> _users;
        public static IList<User> Users
        {
            get { return GetInstance()._users; }
        }

        private UserRepository()
        {
            _users = new List<User>();
        }

        private static UserRepository GetInstance()
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new UserRepository();
                }
            }
            return _instance;
        }

        public static IEnumerable<User> GetAll(Func<User, bool> filter = null)
        {
            IEnumerable<User> usersToReturn = Users;
            if (filter is not null)
            {
                usersToReturn = usersToReturn.Where(filter);
            }
            return usersToReturn;
        }
        
        public static User Get(Func<User, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }
        
        public static void Add(User user)
        {
            lock (Users)
            {
                Users.Add(user);
            }
        }
        
        public static void Remove(User user)
        {
            lock (Users)
            {
                Users.Remove(user);
            }
        }
    }
}
