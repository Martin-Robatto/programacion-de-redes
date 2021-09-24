using System;

namespace Domain
{
    public class Publish
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        
        public Game Game { get; set; }
        public User User { get; set; }
        
        public Publish() { }
        
        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Publish publish)
            {
                result = this.User.Equals(publish.User) && this.Game.Equals(publish.Game);
            }

            return result;
        }
    }
}