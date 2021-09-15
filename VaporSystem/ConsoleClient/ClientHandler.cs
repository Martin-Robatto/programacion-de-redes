using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ConsoleClient.Function;
using SettingsLogic;
using SettingsLogic.Interface;

namespace ConsoleClient
{
    public class ClientHandler
    {
        public static bool Exit { get; set; }
        private static Dictionary<int, FunctionInterface.IFunction> _functions;
        private static readonly ISettingsManager _settingsManager = new SettingsManager();

        public static void Run()
        {
            Exit = false;
            var tcpClient = Initialize();
            Connect(tcpClient);
            using (var networkStream = tcpClient.GetStream())
            {
                while (!Exit)
                {
                    ClientDisplay.MainMenu();
                    var userInput = Console.ReadLine();
                    Execute(networkStream, userInput);
                }
            }
            tcpClient.Close();
        }

        private static TcpClient Initialize()
        {
            var clientIpAddress = _settingsManager.ReadSetting(ClientConfig.ClientIpConfigKey);
            var clientPort = GetPort();
            ClientDisplay.Starting(clientIpAddress, clientPort);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(clientIpAddress), clientPort);
            var tcpClient = new TcpClient(ipEndPoint);
            return tcpClient;
        }

        private static int GetPort()
        {
            var random = new Random();
            var port = random.Next(20001, 30001);
            return port;
        }
        
        private static void Connect(TcpClient tcpClient)
        {
            var serverIpAddress = _settingsManager.ReadSetting(ClientConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ClientConfig.ServerPortConfigKey);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            tcpClient.Connect(ipEndPoint);
            ClientDisplay.Connected();
        }
        
        private static void Execute(NetworkStream stream, string userInput)
        {
            _functions = FunctionDictionary.Get();
            int commandID = Int32.Parse(userInput);
            var command = _functions[commandID];
            command.Execute(stream);
        }

    }
}