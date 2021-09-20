using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ConsoleServer.Function;
using Protocol;
using SettingsLogic;
using SettingsLogic.Interface;
using SocketLogic;

namespace ConsoleServer
{
    public class ServerHandler
    {
        private static bool _exit;
        private static Dictionary<int, FunctionTemplate> _functions;
        private static readonly ISettingsManager _settingsManager = new SettingsManager();
        
        public static void Run()
        {
            _exit = false;
            var tcpListener = Initialize();
            var thread = new Thread(() => ListenForConnections(tcpListener));
            thread.IsBackground = true;
            thread.Start();
            ServerDisplay.Listening();
            ServerDisplay.MainMenu();
            while (!_exit)
            {
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "0":
                        ShutDown(tcpListener);
                        break;
                    default:
                        ServerDisplay.InvalidOption();
                        break;
                }
            }
        }

        private static TcpListener Initialize()
        {
            var serverIpAddress = _settingsManager.ReadSetting(ServerConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ServerConfig.ServerPortConfigKey);
            ServerDisplay.Starting(serverIpAddress, serverPort);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            var tcpListener = new TcpListener(ipEndPoint);
            tcpListener.Start(100);
            return tcpListener;
        }

        private static void ListenForConnections(TcpListener tcpListener)
        {
            while (!_exit)
            {
                try
                {
                    var tcpClientSocket = tcpListener.AcceptTcpClient();
                    ServerDisplay.NewConnection();
                    var thread = new Thread(() => Handle(tcpClientSocket));
                    thread.IsBackground = true;
                    thread.Start();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    _exit = true;
                }
            }
        }

        private static void Handle(TcpClient tcpClientSocket)
        {
            var isClientConnected = true;
            try
            {
                var buffer = new byte[HeaderConstants.HEADER_LENGTH];
                var networkStream = tcpClientSocket.GetStream();
                while (isClientConnected && !_exit)
                {
                    SocketManager.Receive(networkStream, HeaderConstants.HEADER_LENGTH, buffer);
                    var header = new Header(buffer);
                    if (header.Command == FunctionConstants.EXIT)
                    {
                        isClientConnected = false;
                        ServerDisplay.ConnectionInterrupted();
                    }
                    ExecuteFunction(networkStream, header);
                }
            }
            catch (SocketException)
            {
                ServerDisplay.ConnectionInterrupted();
            }
        }

        private static void ExecuteFunction(NetworkStream networkStream, Header header)
        {
            _functions = FunctionDictionary.Get();
            var command = _functions[header.Command];
            command.Execute(networkStream, header: header);
        }

        private static void ShutDown(TcpListener tcpListener)
        {
            _exit = true;
            tcpListener.Stop();
            ServerDisplay.Closing();
        }
    }
}
