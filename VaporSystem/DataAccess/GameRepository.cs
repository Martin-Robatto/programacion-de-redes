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
