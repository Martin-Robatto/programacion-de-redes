using ConsoleClient.Function.Menu;
using ConsoleClient.Function.Purchase;
using ConsoleClient.Function.Review;
using FunctionInterface;
using Protocol;
using System.Collections.Generic;
using ConsoleClient.Function.File;

namespace ConsoleClient.Function
{
    public class FunctionDictionary
    {

        public FunctionDictionary() { }
        
        public Dictionary<int, IClientFunction> NoConnection()
        {
            var commands = new Dictionary<int, IClientFunction> {{FunctionConstants.EXIT, new ExitFunction()}};
            return commands;
        }

        public Dictionary<int, IClientFunction> LogIn()
        {
            var commands = new Dictionary<int, IClientFunction>
            {
                {FunctionConstants.POST_USER, new RegisterFunction()},
                {FunctionConstants.LOGIN, new LogInFunction()},
                {FunctionConstants.EXIT, new ExitFunction()}
            };
            return commands;
        }

        public Dictionary<int, IClientFunction> Main()
        {
            var commands = new Dictionary<int, IClientFunction>
            {
                {1, new GoToGamesMenuFunction()},
                {2, new GoToPublishesMenuFunction()},
                {3, new GoToPurchasesMenuFunction()},
                {4, new GoToReviewsMenuFunction()},
                {FunctionConstants.EXIT, new ExitFunction()}
            };
            return commands;
        }

        public Dictionary<int, IClientFunction> Games()
        {
            var commands = new Dictionary<int, IClientFunction>
            {
                {FunctionConstants.GET_ALL_GAMES, new GetAllGamesFunction()},
                {FunctionConstants.GET_GAME_BY_TITLE, new GetGameByTitleFunction()},
                {FunctionConstants.GET_GAME_BY_CATEGORY, new GetGameByCategoryFunction()},
                {FunctionConstants.GET_GAME_BY_RATE, new GetGameByRateFunction()},
                {FunctionConstants.GET_FILE, new GetFileFunction()},
                {FunctionConstants.EXIT, new BackToMainMenuFunction()}
            };
            return commands;
        }

        public Dictionary<int, IClientFunction> Publishes()
        {
            var commands = new Dictionary<int, IClientFunction>
            {
                {FunctionConstants.GET_PUBLISHES_BY_USER, new GetPublishesByUserFunction()},
                {FunctionConstants.POST_PUBLISH, new PostPublishFunction()},
                {FunctionConstants.POST_FILE, new PostFileFunction()},
                {FunctionConstants.DELETE_PUBLISH, new DeletePublishFunction()},
                {FunctionConstants.PUT_PUBLISH, new PutPublishFunction()},
                {FunctionConstants.EXIT, new BackToMainMenuFunction()}
            };
            return commands;
        }

        public Dictionary<int, IClientFunction> Purchases()
        {
            var commands = new Dictionary<int, IClientFunction>
            {
                {FunctionConstants.GET_PURCHASES_BY_USER, new GetPurchasesByUserFunction()},
                {FunctionConstants.POST_PURCHASE, new PostPurchaseFunction()},
                {FunctionConstants.DELETE_PURCHASE, new DeletePurchaseFunction()},
                {FunctionConstants.EXIT, new BackToMainMenuFunction()}
            };
            return commands;
        }

        public Dictionary<int, IClientFunction> Reviews()
        {
            var commands = new Dictionary<int, IClientFunction>
            {
                {FunctionConstants.GET_REVIEWS_BY_USER, new GetReviewsByUserFunction()},
                {FunctionConstants.GET_REVIEWS_BY_GAME, new GetReviewsByGameFunction()},
                {FunctionConstants.POST_REVIEW, new PostReviewFunction()},
                {FunctionConstants.DELETE_REVIEW, new DeleteReviewFunction()},
                {FunctionConstants.PUT_REVIEW, new PutReviewFunction()},
                {FunctionConstants.EXIT, new BackToMainMenuFunction()}
            };
            return commands;
        }
    }
}