using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User() { }
        
        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is User user)
            {
                result = this.Username.Equals(user.Username);
            }

            return result;
        }
    }
}