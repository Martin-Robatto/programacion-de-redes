using System;
using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public float Rate { get; set; }
        public string Synopsis { get; set; }
        public string Picture { get; set; }
        public long FileSize { get; set; }

        public Game() { }
        
        public override bool Equals(object obj)
        {
            bool result = false;

            if (obj is Game game)
            {
                result = this.Title == game.Title;
            }

            return result;
        }
    }
}