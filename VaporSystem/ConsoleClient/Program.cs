using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ConsoleClient.Function;
using ConsoleDisplay;

namespace ConsoleClient
{
    class Program
    {
        
        private static bool _exit = false;
        private static Socket _socket;
        private static Dictionary<int, FunctionTemplate> _functions;
        
        static void Main(string[] args)
        {
            try
            {
                InitializeSocket();
                _socket.Connect("127.0.0.1", 20000);
                while (!_exit)
                {
                    Print.MainClientMenu();
                    var userInput = Console.ReadLine();
                    if (userInput.Equals("exit"))
                    {
                        ShutDown();
                    }
                    else
                    {
                        _functions = FunctionDictionary.Get();
                        int commandID = Int32.Parse(userInput);
                        var command = _functions[commandID];
                        command.Execute(_socket);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void InitializeSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0));
        }
        
        private static void ShutDown()
        {
            _exit = true;
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
    }
}