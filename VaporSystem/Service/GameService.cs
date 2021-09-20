using System;
using DataAccess;
using Domain;
using System.Collections.Generic;
using System.Linq;
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

        public Game Save(string gameLine)
        {
            string[] gameAttributes = gameLine.Split("#");
            Game inputGame = new Game()
            {
                Id = Guid.NewGuid(),
                Title = gameAttributes[0],
                Genre = gameAttributes[1],
                Synopsis = gameAttributes[2],
                Rate = 0
            };
            GameRepository.Get().Add(inputGame);
            Console.WriteLine($"Nuevo juego: {inputGame.Title}");
            return inputGame;
        }

        public Game Get(string title)
        {
            Game game = GameRepository.Get().FirstOrDefault(game => game.Title.Equals(title));
            if (game is null)
            {
                throw new NotFoundException(title);
            }
            return game;
        }
    }
}