using System;

namespace Domain
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        
        public Game Game { get; set; }
        public User User { get; set; }
        
        public Review() { }
        
        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Review review)
            {
                result = this.User.Equals(review.User) && this.Game.Equals(review.Game);
            }

            return result;
        }
    }
}