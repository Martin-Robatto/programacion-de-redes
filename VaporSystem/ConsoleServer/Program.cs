﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ConsoleDisplay;
using ConsoleServer.Function;
using Protocol;
using SettingsManager.Interface;
using SocketLogic;

namespace ConsoleServer
{
    class Program
    {
        
        private static bool _exit = false;
        private static Socket _socket;
        private static ICollection<Socket> _clients = new List<Socket>();
        private static Dictionary<int, FunctionTemplate> _functions;
        private static readonly ISettingsManager _settingsManager = new SettingsManager.SettingsManager();

        static void Main(string[] args)
        {
            try
            {
                InitializeSocket();
                Print.MainServerMenu();
                while (!_exit)
                {
                    var userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "exit":
                            ShutDown();
                            break;
                        default:
                            Console.WriteLine("Opción inválida");
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                ShutDown();
            }
        }

        private static void InitializeSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var serverIpAddress = _settingsManager.ReadSetting(ServerConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ServerConfig.ServerPortConfigKey);
            Console.WriteLine(
                $"Server is starting listening on IP: {serverIpAddress} and port {serverPort}");

            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            _socket.Bind(ipEndPoint);
            _socket.Listen(100);

            var thread = new Thread(() => ListenForConnections());
            thread.Start();
        }

        private static void ListenForConnections()
        {
            while (!_exit)
            {
                try
                {
                    var clientConnected = _socket.Accept();
                    Console.WriteLine($"Nueva conexión aceptada");
                    _clients.Add(clientConnected);
                    var thread = new Thread(() => Handle(clientConnected));
                    thread.Start();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    _exit = true;
                }
            }
        }
        
        private static void Handle(Socket socket)
        {
            while (!_exit)
            {
                var buffer = new byte[HeaderConstants.HeaderLength];
                try
                {
                    SocketManager.Receive(socket, HeaderConstants.HeaderLength, buffer);
                    var header = new Header();
                    header.DecodeData(buffer);
                    _functions = FunctionDictionary.Get();
                    var command = _functions[header.Command];
                    command.Execute(socket, header);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
        
        private static void ShutDown()
        {
            _exit = true;
            _socket.Close(0);
            foreach (var client in _clients)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            var fakeSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            fakeSocket.Connect("127.0.0.1", 20000);
        }
    }
}