using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public float Rate { get; set; }
        public string Synopsis { get; set; }
        public string Picture { get; set; }
        
        public ICollection<User> Users { get; set; }
        public ICollection<Publish> Publishes { get; set; }
        public ICollection<Review> Reviews { get; set; }
        
        public Game() { }
    }
}