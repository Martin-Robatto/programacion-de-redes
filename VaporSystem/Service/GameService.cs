using DataAccess;
using Domain;
using System.Collections.Generic;

namespace Service
{
    public class GameService
    {
        private static GameService _instance;
        public static GameService Instance
        {
            get { return GetInstance(); }
        }

        private static GameService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new GameService();
            }
            return _instance;
        }

        private GameService() { }
        public List<string> GetGames()
        {
            List<string> games_in_stock = new List<string>();
            foreach (Game game in GameRepository.Get())
            {
                games_in_stock.Add(game.Title);
            }
            return games_in_stock;
        }
    }
}