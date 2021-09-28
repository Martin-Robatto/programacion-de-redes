using System.Collections.Generic;
using ConsoleClient.Function.File;
using ConsoleClient.Function.Menu;
using ConsoleClient.Function.Purchase;
using ConsoleClient.Function.Review;
using FunctionInterface;
using Protocol;

namespace ConsoleClient.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, IClientFunction> NoConnection()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(FunctionConstants.EXIT, new ExitFunction());
            return commands;
        }
        
        public static Dictionary<int, IClientFunction> LogIn()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(FunctionConstants.REGISTER, new RegisterFunction());
            commands.Add(FunctionConstants.LOGIN, new LogInFunction());
            commands.Add(FunctionConstants.EXIT, new ExitFunction());
            return commands;
        }
        
        public static Dictionary<int, IClientFunction> Main()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(1, new GoToGamesMenuFunction());
            commands.Add(2, new GoToPublishesMenuFunction());
            commands.Add(3, new GoToPurchasesMenuFunction());
            commands.Add(4, new GoToReviewsMenuFunction());
            commands.Add(FunctionConstants.EXIT, new ExitFunction());
            return commands;
        }
        
        public static Dictionary<int, IClientFunction> Games()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(FunctionConstants.GET_ALL_GAMES, new GetAllGamesFunction());
            commands.Add(FunctionConstants.GET_GAME_BY_TITLE, new GetGameByTitleFunction());
            commands.Add(FunctionConstants.GET_GAME_BY_CATEGORY, new GetGameByCategoryFunction());
            commands.Add(FunctionConstants.GET_GAME_BY_RATE, new GetGameByRateFunction());
            commands.Add(FunctionConstants.EXIT, new BackToMainMenuFunction());
            return commands;
        }
        
        public static Dictionary<int, IClientFunction> Publishes()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(FunctionConstants.GET_PUBLISHES_BY_USER, new GetPublishesByUserFunction());
            commands.Add(FunctionConstants.POST_PUBLISH, new PostPublishFunction());
            commands.Add(FunctionConstants.DELETE_PUBLISH, new DeletePublishFunction());
            commands.Add(FunctionConstants.PUT_PUBLISH, new PutPublishFunction());
            commands.Add(FunctionConstants.EXIT, new BackToMainMenuFunction());
            return commands;
        }
        
        public static Dictionary<int, IClientFunction> Purchases()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(FunctionConstants.GET_PURCHASES_BY_USER, new GetPurchasesByUserFunction());
            commands.Add(FunctionConstants.POST_PURCHASE, new PostPurchaseFunction());
            commands.Add(FunctionConstants.DELETE_PURCHASE, new DeletePurchaseFunction());
            commands.Add(FunctionConstants.EXIT, new BackToMainMenuFunction());
            return commands;
        }
        
        public static Dictionary<int, IClientFunction> Reviews()
        {
            var commands = new Dictionary<int, IClientFunction>();
            commands.Add(FunctionConstants.GET_REVIEWS_BY_USER, new GetReviewsByUserFunction());
            commands.Add(FunctionConstants.GET_REVIEWS_BY_GAME, new GetReviewsByGameFunction());
            commands.Add(FunctionConstants.POST_REVIEW, new PostReviewFunction());
            commands.Add(FunctionConstants.DELETE_REVIEW, new DeleteReviewFunction());
            commands.Add(FunctionConstants.PUT_REVIEW, new PutReviewFunction());
            commands.Add(FunctionConstants.EXIT, new BackToMainMenuFunction());
            return commands;
        }
    }
}