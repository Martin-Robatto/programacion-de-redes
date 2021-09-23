using System.Collections.Generic;
using ConsoleServer.Function;
using ConsoleServer.Function.Review;
using Protocol;

namespace ConsoleServer.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, FunctionTemplate> Get()
        {
            var commands = new Dictionary<int, FunctionTemplate>();
            commands.Add(FunctionConstants.EXIT, new ExitFunction());
            commands.Add(FunctionConstants.MESSAGE, new MessageFunction());
            commands.Add(FunctionConstants.REGISTER, new RegisterFunction());
            commands.Add(FunctionConstants.LOGIN, new LogInFunction());
            commands.Add(FunctionConstants.GET_ALL_GAMES, new GetAllGamesFunction());
            commands.Add(FunctionConstants.POST_PUBLISH, new PostPublishFunction());
            commands.Add(FunctionConstants.GET_PUBLISHES_BY_USER, new GetPublishesByUserFunction());
            commands.Add(FunctionConstants.POST_PURCHASE, new PostPurchaseFunction());
            commands.Add(FunctionConstants.GET_PURCHASES_BY_USER, new GetPurchasesByUserFunction());
            commands.Add(FunctionConstants.POST_REVIEW, new PostReviewFunction());
            commands.Add(FunctionConstants.GET_REVIEWS_BY_USER, new GetReviewsByUserFunction());
            return commands;
        }
    }
}