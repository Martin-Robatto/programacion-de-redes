using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ConsoleClient.Function;
using FunctionInterface;
using SettingsLogic;
using SettingsLogic.Interface;

namespace ConsoleClient
{
    public class ClientHandler
    {
        public static bool Exit { get; set; }
        private static string _actualSession = string.Empty;
        private static Dictionary<int, IClientFunction> _actualFunctions;
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
                        _actualFunctions = FunctionDictionary.LogIn();
                    }
                    ClientDisplay.Menu(_actualFunctions);
                    var userInput = Console.ReadLine();
                    int input = int.TryParse(userInput, out input) ? input : 1000;
                    if (_actualFunctions.ContainsKey(input))
                    {
                        var command = _actualFunctions[input];
                        command.Execute(networkStream, session: _actualSession);
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

        public static void SetActualSession(string user)
        {
            _actualSession = user;
            SetActualFunctions(FunctionDictionary.Main()); 
        }
        
        public static void SetActualFunctions(Dictionary<int, IClientFunction> functions)
        {
            _actualFunctions = functions;
        }
    }
}