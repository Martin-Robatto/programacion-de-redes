using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Publish> Publishes { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public User() { }
    }
}