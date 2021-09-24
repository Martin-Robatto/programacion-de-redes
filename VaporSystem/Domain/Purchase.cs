using System;

namespace Domain
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        
        public Game Game { get; set; }
        public User User { get; set; }
        
        public Purchase() { }
        
        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Purchase purchase)
            {
                result = this.User.Equals(purchase.User) && this.Game.Equals(purchase.Game);
            }

            return result;
        }
    }
}