using System.Collections.Generic;
using ConsoleClient.Function.Purchase;
using ConsoleClient.Function.Review;
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
            commands.Add(FunctionConstants.GET_GAME_BY_TITLE, new GetGameByTitleFunction());
            commands.Add(FunctionConstants.GET_GAME_BY_CATEGORY, new GetGameByCategoryFunction());
            commands.Add(FunctionConstants.GET_GAME_BY_RATE, new GetGameByRateFunction());
            commands.Add(FunctionConstants.GET_PUBLISHES_BY_USER, new GetPublishesByUserFunction());
            commands.Add(FunctionConstants.POST_PUBLISH, new PostPublishFunction());
            commands.Add(FunctionConstants.DELETE_PUBLISH, new DeletePublishFunction());
            commands.Add(FunctionConstants.PUT_PUBLISH, new PutPublishFunction());
            commands.Add(FunctionConstants.GET_PURCHASES_BY_USER, new GetPurchasesByUserFunction());
            commands.Add(FunctionConstants.POST_PURCHASE, new PostPurchaseFunction());
            commands.Add(FunctionConstants.DELETE_PURCHASE, new DeletePurchaseFunction());
            commands.Add(FunctionConstants.GET_REVIEWS_BY_USER, new GetReviewsByUserFunction());
            commands.Add(FunctionConstants.POST_REVIEW, new PostReviewFunction());
            commands.Add(FunctionConstants.DELETE_REVIEW, new DeleteReviewFunction());
            commands.Add(FunctionConstants.PUT_REVIEW, new PutReviewFunction());
            commands.Add(FunctionConstants.EXIT, new ExitFunction());
            return commands;
        }
    }
}