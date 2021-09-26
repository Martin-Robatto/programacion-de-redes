﻿using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain;
using Exceptions;

namespace Service
{
    public class GameValidator
    {
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
        
        public void CheckGameAlreadyExists(Game input)
        {
            var game = GameRepository.Get(g => g.Equals(input));
            if (game is not null)
            {
                throw new AlreadyExistsException("Game");
            }
        }
    }
}