using System.Collections.Generic;
using FunctionInterface;
using Protocol;

namespace ConsoleServer.Function
{
    public class FunctionDictionary
    {
        public static Dictionary<int, IFunction> Get()
        {
            var commands = new Dictionary<int, IFunction>();
            commands.Add(FunctionConstants.Message, new MessageFunction());
            return commands;
        }
    }
}