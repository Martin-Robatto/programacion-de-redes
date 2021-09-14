using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ConsoleServer.Function;
using Protocol;
using SettingsManager.Interface;
using SocketLogic;

namespace ConsoleServer
{
    public class ServerHandler
    {
        public static bool Exit { get; set; }
        private static Socket _socket;
        private static ICollection<Socket> _clients = new List<Socket>();
        private static Dictionary<int, FunctionTemplate> _functions;
        private static readonly ISettingsManager _settingsManager = new SettingsManager.SettingsManager();
        
        public static void InitializeSocket()
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

        public static void ListenForConnections()
        {
            while (!Exit)
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
                    Exit = true;
                }
            }
        }
        
        public static void Handle(Socket socket)
        {
            while (!Exit)
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
        
        public static void ShutDown()
        {
            Exit = true;
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
