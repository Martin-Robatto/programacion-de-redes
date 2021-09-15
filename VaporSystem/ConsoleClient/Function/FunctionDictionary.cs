using System.Collections.Generic;
using Protocol;

namespace ConsoleClient.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, IFunction.IFunction> Get()
        {
            var commands = new Dictionary<int, IFunction.IFunction>();
            commands.Add(FunctionConstants.Message, new MessageFunction());
            commands.Add(FunctionConstants.Register, new RegisterFunction());
            commands.Add(FunctionConstants.GetAllGames, new GetAllGamesFunction());
            commands.Add(FunctionConstants.Exit, new ExitFunction());
            return commands;
        }
    }
}