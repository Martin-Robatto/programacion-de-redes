using System.Collections.Generic;
using Protocol;

namespace ConsoleServer.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, FunctionTemplate> Get()
        {
            var commands = new Dictionary<int, FunctionTemplate>();
            commands.Add(FunctionConstants.MESSAGE, new MessageFunction());
            commands.Add(FunctionConstants.REGISTER, new RegisterFunction());
            commands.Add(FunctionConstants.GET_ALL_GAMES, new GetAllGamesFunction());
            commands.Add(FunctionConstants.EXIT, new ExitFunction());
            return commands;
        }
    }
}