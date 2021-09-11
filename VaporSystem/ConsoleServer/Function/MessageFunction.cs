using System;
using System.Net.Sockets;
using System.Text;
using FunctionInterface;
using Protocol;
using SocketLogic;

namespace ConsoleServer.Function
{
    public class MessageFunction : IFunction
    {
        public void Execute(Socket socket, Header header)
        {
            var bufferData = new byte[header.DataLength];  
            SocketManager.Receive(socket, header.DataLength, bufferData);
            Console.WriteLine("Message received: " + Encoding.UTF8.GetString(bufferData));
        }
    }
}