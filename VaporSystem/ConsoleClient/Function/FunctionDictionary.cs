using System.Collections.Generic;
using FunctionInterface;
using Protocol;

namespace ConsoleClient.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, IClientFunction> NoRequiresCredentials()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(FunctionConstants.REGISTER, new RegisterFunction());
            commands.Add(FunctionConstants.LOGIN, new LogInFunction());
            commands.Add(FunctionConstants.EXIT, new ExitFunction());
            return commands;
        }
        
        public static Dictionary<int, IClientFunction> RequiresCredentials()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(FunctionConstants.GET_ALL_GAMES, new GetAllGamesFunction());
            commands.Add(FunctionConstants.GET_PUBLISHES_BY_USER, new GetPublishesByUserFunction());
            commands.Add(FunctionConstants.POST_PUBLISH, new PostPublishFunction());
            commands.Add(FunctionConstants.EXIT, new ExitFunction());
            return commands;
        }
    }
}