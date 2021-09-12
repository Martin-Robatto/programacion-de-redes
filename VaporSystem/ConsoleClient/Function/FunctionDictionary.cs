using System.Collections.Generic;
using Protocol;

namespace ConsoleClient.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, FunctionTemplate> Get()
        {
            var commands = new Dictionary<int, FunctionTemplate>();
            commands.Add(FunctionConstants.Message, new MessageFunction());
            commands.Add(FunctionConstants.Register, new RegisterFunction());
            return commands;
        }
    }
}