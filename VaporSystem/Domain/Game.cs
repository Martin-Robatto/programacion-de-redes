using System;

namespace Domain
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public float Rate { get; set; }
        public string Synopsis { get; set; }
        public string PicturePath { get; set; }

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