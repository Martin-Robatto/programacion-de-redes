using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ConsoleClient.Function;
using SettingsManager.Interface;

namespace ConsoleClient
{
    public class ClientHandler
    {
        public static bool Exit { get; set; }
        private static Socket _socket;
        private static Dictionary<int, IFunction.IFunction> _functions;
        private static readonly ISettingsManager _settingsManager = new SettingsManager.SettingsManager();
        
        public static void Connection()
        {
            var serverIpAddress = _settingsManager.ReadSetting(ClientConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ClientConfig.ServerPortConfigKey);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            _socket.Connect(ipEndPoint);
        }

        public static void InitializeSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var clientIpAddress = _settingsManager.ReadSetting(ClientConfig.ClientIpConfigKey);
            var clientPort = _settingsManager.ReadSetting(ClientConfig.ClientPortConfigKey);
            Console.WriteLine(
                $"Client is starting listening on IP: {clientIpAddress} and port {clientPort}");

            var ipEndPoint = new IPEndPoint(IPAddress.Parse(clientIpAddress), int.Parse(clientPort));
            _socket.Bind(ipEndPoint);
        }
        
        public static void ShutDown()
        {
            Exit = true;
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
        
        public static void Execute(string userInput)
        {
            _functions = FunctionDictionary.Get();
            int commandID = Int32.Parse(userInput);
            var command = _functions[commandID];
            command.Execute(_socket);
        }
    }
}