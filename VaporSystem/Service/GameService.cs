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
            IEnumerable<Game> games = GameRepository.GetAll();
            if (!games.Any())
            {
                throw new NotFoundException("Games");
            }
            string gamesLine = string.Empty;
            foreach (Game game in games)
            {
                gamesLine += "#" + game.Title;
            }
            return gamesLine;
        }

        public Game Get(string title)
        {
            Game game = GameRepository.Get(g => g.Title.Equals(title));
            if (game is null)
            {
                throw new NotFoundException("Game");
            }
            return game;
        }
        
        public Game Save(string gameLine)
        {
            string[] attributes = gameLine.Split("#");
            Game input = new Game()
            {
                Id = Guid.NewGuid(),
                Title = attributes[0],
                Genre = attributes[1],
                Synopsis = attributes[2],
                Rate = 0
            };
            var game = GameRepository.Get(g => g.Equals(input));
            if (game is not null)
            {
                throw new AlreadyExistsException("Game");
            }
            GameRepository.Add(input);
            Console.WriteLine($"Nuevo juego: {input.Title}");
            return input;
        }
    }
}