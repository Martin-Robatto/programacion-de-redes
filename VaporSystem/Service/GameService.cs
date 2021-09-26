using System;
using DataAccess;
using Domain;
using System.Collections.Generic;
using System.Linq;
using Exceptions;
using Protocol;

namespace Service
{
    public class GameService
    {
        private static GameService _instance;
        private GameValidator _validator;
        
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

        private GameService()
        {
            _validator = new GameValidator();
        }
        
        public Game Get(string title)
        {
            Game game = GameRepository.Get(g => g.Title.Equals(title));
            _validator.CheckGameIsNull(game);
            return game;
        }

        public IEnumerable<Game> GetAll(Func<Game, bool> filter = null)
        {
            IEnumerable<Game> games = GameRepository.GetAll(filter);
            _validator.CheckGamesAreEmpty(games);
            return games;
        }

        public string GetGames()
        {
            IEnumerable<Game> games = GetAll();
            return SimpleFormat(games);
        }

        public string GetByTitle(string titleLine)
        {
            IEnumerable<Game> games = new List<Game>();
            string gamesLine = string.Empty;
            string[] attributes = titleLine.Split("&");
            games = GetAll(g => g.Title.Equals(attributes[1]));
            gamesLine = FullFormat(games);
            return gamesLine;
        }
        
        public string GetByCategory(string categoryLine)
        {
            IEnumerable<Game> games = new List<Game>();
            string gamesLine = string.Empty;
            string[] attributes = categoryLine.Split("&");
            games = GetAll(g => g.Genre.Equals(attributes[1]));
            gamesLine = FullFormat(games);
            return gamesLine;
        }
        
        public string GetByRate(string rateLine)
        {
            IEnumerable<Game> games = new List<Game>();
            string gamesLine = string.Empty;
            string[] attributes = rateLine.Split("&");
            float rate = float.Parse(attributes[1]);
            games = GetAll(g => g.Rate.Equals(rate));
            gamesLine = FullFormat(games);
            return gamesLine;
        }

        private string SimpleFormat(IEnumerable<Game> games)
        {
            string gamesLine = string.Empty;
            int index = 0;
            foreach (Game game in games)
            {
                if (index > 0)
                {
                    gamesLine += $"#";
                }
                gamesLine += game.Title;
                index++;
            }
            return gamesLine;
        }
        
        private string FullFormat(IEnumerable<Game> games)
        {
            string gamesLine = string.Empty;
            int index = 0;
            foreach (Game game in games)
            {
                if (index > 0)
                {
                    gamesLine += $"&";
                }
                gamesLine += $"{game.Title}#{game.Genre}#{game.Synopsis}#{game.Rate}";
                index++;
            }
            return gamesLine;
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
            _validator.CheckGameAlreadyExists(input);
            GameRepository.Add(input);
            Console.WriteLine($"Juego nuevo: {input.Title}");
            return input;
        }

        public void Delete(Game game)
        {
            var aGame = GameRepository.Get(g => g.Equals(game));
            _validator.CheckGameIsNull(aGame);
            GameRepository.Remove(aGame);
            Console.WriteLine($"Juego eliminado: {game.Title}");
        }

        public void Update(string gameLine)
        {
            string[] attributes = gameLine.Split("#");
            var game = Get(attributes[0]);
            game.Genre = attributes[1];
            game.Synopsis = attributes[2];
            GameRepository.Update(game);
            Console.WriteLine($"Juego modificado: {game.Title}");
        }
    }
}