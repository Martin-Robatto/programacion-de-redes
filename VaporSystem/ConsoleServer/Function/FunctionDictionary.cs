using ConsoleServer.Function.File;
using ConsoleServer.Function.Review;
using Protocol;
using System.Collections.Generic;

namespace ConsoleServer.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, FunctionTemplate> Get()
        {
            var commands = new Dictionary<int, FunctionTemplate>();
            commands.Add(FunctionConstants.REGISTER, new RegisterFunction());
            commands.Add(FunctionConstants.LOGIN, new LogInFunction());
            commands.Add(FunctionConstants.GET_ALL_GAMES, new GetAllGamesFunction());
            commands.Add(FunctionConstants.POST_PUBLISH, new PostPublishFunction());
            commands.Add(FunctionConstants.GET_PUBLISHES_BY_USER, new GetPublishesByUserFunction());
            commands.Add(FunctionConstants.POST_PURCHASE, new PostPurchaseFunction());
            commands.Add(FunctionConstants.GET_PURCHASES_BY_USER, new GetPurchasesByUserFunction());
            commands.Add(FunctionConstants.POST_REVIEW, new PostReviewFunction());
            commands.Add(FunctionConstants.DELETE_REVIEW, new DeleteReviewFunction());
            commands.Add(FunctionConstants.GET_REVIEWS_BY_USER, new GetReviewsByUserFunction());
            commands.Add(FunctionConstants.GET_REVIEWS_BY_GAME, new GetReviewsByGameFunction());
            commands.Add(FunctionConstants.DELETE_PURCHASE, new DeletePurchaseFunction());
            commands.Add(FunctionConstants.DELETE_PUBLISH, new DeletePublishFunction());
            commands.Add(FunctionConstants.GET_GAME_BY_TITLE, new GetGameByTitleFunction());
            commands.Add(FunctionConstants.GET_GAME_BY_CATEGORY, new GetGameByCategoryFunction());
            commands.Add(FunctionConstants.GET_GAME_BY_RATE, new GetGameByRateFunction());
            commands.Add(FunctionConstants.PUT_REVIEW, new PutReviewFunction());
            commands.Add(FunctionConstants.PUT_PUBLISH, new PutPublishFunction());
            commands.Add(FunctionConstants.POST_FILE, new PostFileFunction());
            commands.Add(FunctionConstants.GET_FILE, new GetFileFunction());
            return commands;
        }
    }
}