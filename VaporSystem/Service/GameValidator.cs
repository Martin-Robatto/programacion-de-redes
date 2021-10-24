using DataAccess;
using Domain;
using Exceptions;
using FileLogic;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class GameValidator
    {

        private FileManager _fileManager = new FileManager();
        
        public GameValidator() { }

        public void CheckGameIsNull(Game game)
        {
            if (game is null)
            {
                throw new NotFoundException("Game");
            }
        }

        public void CheckGamesAreEmpty(IEnumerable<Game> games)
        {
            if (!games.Any())
            {
                throw new NotFoundException("Games");
            }
        }

        public void CheckAttributesAreEmpty(string[] attributes)
        {
            foreach (var attribute in attributes)
            {
                if (string.IsNullOrEmpty(attribute))
                {
                    throw new InvalidInputException("empty attribute");
                }
            }
        }

        public void CheckGameAlreadyExists(Game input)
        {
            var game = GameRepository.Get(g => g.Equals(input));
            if (game is not null)
            {
                throw new AlreadyExistsException("Game");
            }
        }

        public void CheckGameWithInvalidPicture(Game game)
        {
            if (!_fileManager.FileExists(game.PicturePath) || !_fileManager.IsValidExtension(game.PicturePath))
            {
                throw new NotReadableFileException();
            }
        }
    }
}