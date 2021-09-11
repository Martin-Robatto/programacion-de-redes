using DataAccess;
using Domain;
using System.Collections.Generic;

namespace Service
{
    public class GameService
    {
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
