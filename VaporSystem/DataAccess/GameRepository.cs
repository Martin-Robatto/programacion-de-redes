using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class GameRepository
    {
        private static GameRepository _instance;
        private static readonly object _lock = new object();
        private IList<Game> _games;
        public static IList<Game> Games
        {
            get { return GetInstance()._games; }
        }

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

        public static IEnumerable<Game> GetAll(Func<Game, bool> filter = null)
        {
            lock (Games)
            {
                IEnumerable<Game> gamesToReturn = Games;
                if (filter is not null)
                {
                    gamesToReturn = gamesToReturn.Where(filter);
                }
                return gamesToReturn;
            }
        }

        public static Game Get(Func<Game, bool> filter = null)
        {
            return GetAll(filter).FirstOrDefault();
        }

        public static void Add(Game game)
        {
            lock (Games)
            {
                Games.Add(game);
            }
        }

        public static void Remove(Game game)
        {
            lock (Games)
            {
                Games.Remove(game);
            }
        }
    }
}
