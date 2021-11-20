using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class GameRepository
    {
        private static GameRepository _instance;
        public static GameRepository Instance
        {
            get { return GetInstance(); }
        }
        
        private static readonly object _lock = new object();
        private static IList<Game> _games;

        private GameRepository()
        {
            _games = new List<Game>();
        }

        private static GameRepository GetInstance()
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new GameRepository();
                }
            }
            return _instance;
        }

        public IEnumerable<Game> GetAll(Func<Game, bool> filter = null)
        {
            lock (_games)
            {
                IEnumerable<Game> gamesToReturn = _games;
                if (filter is not null)
                {
                    gamesToReturn = gamesToReturn.Where(filter);
                }
                return gamesToReturn;
            }
        }

        public Game Get(Func<Game, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }

        public void Add(Game game)
        {
            lock (_games)
            {
                _games.Add(game);
            }
        }

        public void Remove(Game game)
        {
            lock (_games)
            {
                _games.Remove(game);
            }
        }
    }
}
