using DataAccess;
using Domain;
using FileLogic;
using System;
using System.Collections.Generic;

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
            _validator.CheckAttributesAreEmpty(attributes);
            games = GetAll(g => g.Title.Equals(attributes[1]));
            gamesLine = FullFormat(games);
            return gamesLine;
        }

        public string GetByCategory(string categoryLine)
        {
            IEnumerable<Game> games = new List<Game>();
            string gamesLine = string.Empty;
            string[] attributes = categoryLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            games = GetAll(g => g.Genre.Equals(attributes[1]));
            gamesLine = FullFormat(games);
            return gamesLine;
        }

        public string GetByRate(string rateLine)
        {
            IEnumerable<Game> games = new List<Game>();
            string gamesLine = string.Empty;
            string[] attributes = rateLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
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
            _validator.CheckAttributesAreEmpty(attributes);
            Game input = new Game()
            {
                Id = Guid.NewGuid(),
                Title = attributes[0],
                Genre = attributes[1],
                Synopsis = attributes[2],
                Rate = 0,
                PicturePath = string.Empty
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
            string[] attributes = gameLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            string[] gameAttributes = attributes[2].Split("#");
            _validator.CheckAttributesAreEmpty(gameAttributes);
            var game = Get(attributes[1]);
            if (game.Title.Equals(gameAttributes[0]))
            {
                _validator.CheckGameAlreadyExists(game);
            }
            game.Title = gameAttributes[0];
            game.Genre = gameAttributes[1];
            game.Synopsis = gameAttributes[2];
            Console.WriteLine($"Juego modificado: {game.Title}");
        }

        public string UploadPicture(string gameLine)
        {
            string[] attributes = gameLine.Split("&");
            _validator.CheckAttributesAreEmpty(attributes);
            Game game = Get(attributes[1]);
            _validator.CheckGameWithInvalidPicture(game);
            long fileSize = FileManager.GetFileSize(game.PicturePath);

            string fileLine = $"{game.PicturePath}#{fileSize}";
            return fileLine;
        }
    }
}