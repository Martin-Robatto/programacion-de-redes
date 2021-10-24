using ConsoleServer.Function.File;
using ConsoleServer.Function.Review;
using Protocol;
using System.Collections.Generic;
using FunctionInterface;

namespace ConsoleServer.Function
{
    public class FunctionDictionary
    {
        public FunctionDictionary() { }
        
        public Dictionary<int, IServerFunction> Get()
        {
            var commands = new Dictionary<int, IServerFunction>
            {
                {FunctionConstants.REGISTER, new RegisterFunction()},
                {FunctionConstants.LOGIN, new LogInFunction()},
                {FunctionConstants.GET_ALL_GAMES, new GetAllGamesFunction()},
                {FunctionConstants.POST_PUBLISH, new PostPublishFunction()},
                {FunctionConstants.GET_PUBLISHES_BY_USER, new GetPublishesByUserFunction()},
                {FunctionConstants.POST_PURCHASE, new PostPurchaseFunction()},
                {FunctionConstants.GET_PURCHASES_BY_USER, new GetPurchasesByUserFunction()},
                {FunctionConstants.POST_REVIEW, new PostReviewFunction()},
                {FunctionConstants.DELETE_REVIEW, new DeleteReviewFunction()},
                {FunctionConstants.GET_REVIEWS_BY_USER, new GetReviewsByUserFunction()},
                {FunctionConstants.GET_REVIEWS_BY_GAME, new GetReviewsByGameFunction()},
                {FunctionConstants.DELETE_PURCHASE, new DeletePurchaseFunction()},
                {FunctionConstants.DELETE_PUBLISH, new DeletePublishFunction()},
                {FunctionConstants.GET_GAME_BY_TITLE, new GetGameByTitleFunction()},
                {FunctionConstants.GET_GAME_BY_CATEGORY, new GetGameByCategoryFunction()},
                {FunctionConstants.GET_GAME_BY_RATE, new GetGameByRateFunction()},
                {FunctionConstants.PUT_REVIEW, new PutReviewFunction()},
                {FunctionConstants.PUT_PUBLISH, new PutPublishFunction()},
                {FunctionConstants.POST_FILE, new PostFileFunction()},
                {FunctionConstants.GET_FILE, new GetFileFunction()}
            };
            return commands;
        }
    }
}