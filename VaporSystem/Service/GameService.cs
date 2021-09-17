using DataAccess;
using Domain;
using System.Collections.Generic;
using Exceptions;

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
        
        public string GetGames()
        {
            string games = string.Empty;
            foreach (Game game in GameRepository.Get())
            {
                games += "#" + game.Title;
            }
            if (GameRepository.Get().Count == 0)
            {
                throw new NotFoundException("Games");
            }
            return games;
        }
    }
}