using Domain;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class GameRepository
    {
        private static GameRepository _instance;
        private IList<Game> _games;

        private GameRepository()
        {
            _games = new List<Game>();
            _games.Add(new Game()
            {
                Title = "1"
            });
            _games.Add(new Game()
            {
                Title = "2"
            });
            _games.Add(new Game()
            {
                Title = "3"
            });
        }

        public static IList<Game> Get()
        {
            return GetInstance()._games;
        }

        private static GameRepository GetInstance()
        {
            if (_instance is null)
            {
                _instance = new GameRepository();
            }
            return _instance;
        }
    }
}
