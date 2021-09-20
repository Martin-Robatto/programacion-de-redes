using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ConsoleClient.Function;
using FunctionInterface;
using Protocol;
using SettingsLogic;
using SettingsLogic.Interface;

namespace ConsoleClient
{
    public class ClientHandler
    {
        public static bool Exit { get; set; }
        private static string _actualSession = string.Empty;
        private static Dictionary<int, IClientFunction> _functions;
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
                    if (String.IsNullOrEmpty(_actualSession))
                    {
                        ShowAccessFunctions(networkStream);
                    }
                    else
                    {
                        ShowMainFunctions(networkStream);
                    }
                    ClientDisplay.Continue();
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
        
        private static void ShowAccessFunctions(NetworkStream networkStream)
        {
            ClientDisplay.LoginMenu();
            var userInput = Console.ReadLine();
            int input = int.TryParse(userInput, out input) ? input : 1000;
            if (FunctionDictionary.NoRequiresCredentials().ContainsKey(input))
            {
                _functions = FunctionDictionary.NoRequiresCredentials();
                var command = _functions[input];
                command.Execute(networkStream, session: _actualSession);
            }
        }

        public static void KeepActualSession(string user)
        {
            _actualSession = user;
        }

        private static void ShowMainFunctions(NetworkStream networkStream)
        {
            ClientDisplay.MainMenu();
            var userInput = Console.ReadLine();
            int input = int.TryParse(userInput, out input) ? input : 1000;
            if (FunctionDictionary.RequiresCredentials().ContainsKey(input))
            {
                _functions = FunctionDictionary.RequiresCredentials();
                var command = _functions[input];
                command.Execute(networkStream, session: _actualSession);
            }
        }

    }
}