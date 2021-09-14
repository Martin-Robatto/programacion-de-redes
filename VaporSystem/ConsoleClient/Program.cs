using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ConsoleClient.Function;
using ConsoleDisplay;
using SettingsManager.Interface;

namespace ConsoleClient
{
    class Program
    {
        
        private static bool _exit = false;
        private static Socket _socket;
        private static Dictionary<int, FunctionTemplate> _functions;
        private static readonly ISettingsManager _settingsManager = new SettingsManager.SettingsManager();
        
        static void Main(string[] args)
        {
            try
            {
                InitializeSocket();
                Connection();
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
                ShutDown();
            }
        }

        private static void Connection()
        {
            var serverIpAddress = _settingsManager.ReadSetting(ClientConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ClientConfig.ServerPortConfigKey);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            _socket.Connect(ipEndPoint);
        }

        private static void InitializeSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var clientIpAddress = _settingsManager.ReadSetting(ClientConfig.ClientIpConfigKey);
            var clientPort = _settingsManager.ReadSetting(ClientConfig.ClientPortConfigKey);
            Console.WriteLine(
                $"Client is starting listening on IP: {clientIpAddress} and port {clientPort}");

            var ipEndPoint = new IPEndPoint(IPAddress.Parse(clientIpAddress), int.Parse(clientPort));
            _socket.Bind(ipEndPoint);
        }
        
        private static void ShutDown()
        {
            _exit = true;
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
    }
}