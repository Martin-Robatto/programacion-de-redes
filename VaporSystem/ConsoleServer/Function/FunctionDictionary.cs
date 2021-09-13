using System.Collections.Generic;
using Protocol;

namespace ConsoleServer.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, FunctionTemplate> Get()
        {
            var commands = new Dictionary<int, FunctionTemplate>();
            commands.Add(FunctionConstants.Message, new MessageFunction());
            commands.Add(FunctionConstants.Register, new RegisterFunction());
            commands.Add(FunctionConstants.GetAllGames, new GetAllGamesFunction());
            return commands;
        }
    }
}