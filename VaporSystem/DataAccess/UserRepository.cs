using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class UserRepository
    {
        private static UserRepository _instance;
        public static UserRepository Instance
        {
            get { return GetInstance(); }
        }
        
        private static readonly object _lock = new object();
        private static IList<User> _users;

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

        public IEnumerable<User> GetAll(Func<User, bool> filter = null)
        {
            lock (_users)
            {
                IEnumerable<User> usersToReturn = _users;
                if (filter is not null)
                {
                    usersToReturn = usersToReturn.Where(filter);
                }
                return usersToReturn;
            }
        }

        public User Get(Func<User, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }

        public void Add(User user)
        {
            lock (_users)
            {
                _users.Add(user);
            }
        }

        public void Remove(User user)
        {
            lock (_users)
            {
                _users.Remove(user);
            }
        }
    }
}
