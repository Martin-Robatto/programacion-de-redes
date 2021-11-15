using System.Threading.Tasks;
using ServerAdmin.Models;

namespace ServerAdmin.Parser
{
    public static class Parser
    {
        public static UserAttributes UserModelToUserAttributes(UserModelIn model_to_parse)
        {
            var user_to_return = new UserAttributes()
            {
                UserName = model_to_parse.Username,
                UserPassword = model_to_parse.Password
            };
            return user_to_return;
        }
        
        public static UserModelOut UserAttributesToUserModelOut(UserAttributes attributes)
        {
            var user_to_return = new UserModelOut()
            {
                Username = attributes.UserName
            };
            return user_to_return;
        }
        
        public static GameAttributes GameModelInToGameAttributes(GameModelIn model_to_parse)
        {
            var game_to_return = new GameAttributes()
            {
                Title = model_to_parse.Title,
                Genre = model_to_parse.Genre,
                Synopisis = model_to_parse.Synopsis,
            };
            return game_to_return;
        }
        
        public static GameModelOut GameAttributesToGameModelOut(GameAttributes attributes)
        {
            var game_to_return = new GameModelOut()
            {
                Title = attributes.Title
            };
            return game_to_return;
        }
    }
}