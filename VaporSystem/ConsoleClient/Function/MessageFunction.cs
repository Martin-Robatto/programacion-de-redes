using System;
using System.Net;
using System.Net.Sockets;
using FunctionInterface;
using Protocol;
using SocketLogic;

namespace ConsoleClient.Function
{
    public class MessageFunction : IFunction
    {
        public void Execute(Socket socket, Header header)
        {
            Console.WriteLine("Ingrese el mensaje:");
            var message = Console.ReadLine();
            var newHeader = new Header(HeaderConstants.Request, FunctionConstants.Message, message.Length);
            SocketManager.Send(socket, newHeader, message);
        }
    }
}