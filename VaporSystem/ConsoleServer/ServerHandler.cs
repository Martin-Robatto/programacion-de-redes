using System;
using System.Collections.Generic;
using System.IO;
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
        private static TcpListener _tcpListenerSocket;
        private static Dictionary<TcpClient, NetworkStream> _clientSockets;
        private static readonly ISettingsManager _settingsManager = new SettingsManager();
        
        public static void Run()
        {
            _exit = false;
            try
            {
                _tcpListenerSocket = Initialize();
                _clientSockets = new Dictionary<TcpClient, NetworkStream>();
                var thread = new Thread(() => ListenForConnections());
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
                            ShutDown();
                            break;
                        default:
                            ServerDisplay.InvalidOption();
                            break;
                    }
                }
            }
            catch (Exception) { }
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

        private static void ListenForConnections()
        {
            while (!_exit)
            {
                var tcpClientSocket = _tcpListenerSocket.AcceptTcpClient();
                var networkStream = tcpClientSocket.GetStream();
                _clientSockets.Add(tcpClientSocket, networkStream);
                ServerDisplay.NewConnection();
                var thread = new Thread(() => Handle(tcpClientSocket, networkStream));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private static void Handle(TcpClient tcpClientSocket, NetworkStream networkStream)
        {
            var isClientConnected = true;
            try
            {
                while (isClientConnected && !_exit)
                {
                    var buffer = SocketManager.Receive(networkStream, HeaderConstants.HEADER_LENGTH);
                    var header = new Header(buffer);
                    if (header.Command == FunctionConstants.EXIT)
                    {
                        isClientConnected = false;
                        ServerDisplay.ConnectionInterrupted();
                        _clientSockets.Remove(tcpClientSocket);
                    }
                    ExecuteFunction(networkStream, header);
                }
            }
            catch (IOException)
            {
                ServerDisplay.ConnectionInterrupted();
                _clientSockets.Remove(tcpClientSocket);
            }
            catch (SocketException)
            {
                ServerDisplay.ConnectionInterrupted();
                _clientSockets.Remove(tcpClientSocket);
            }
        }

        private static void ExecuteFunction(NetworkStream networkStream, Header header)
        {
            _functions = FunctionDictionary.Get();
            var command = _functions[header.Command];
            command.Execute(networkStream, header: header);
        }

        private static void ShutDown()
        {
            _tcpListenerSocket.Stop();
            foreach (var client in _clientSockets)
            {
                client.Value.Close();
                client.Key.Close();
            }
            _clientSockets.Clear();
            ServerDisplay.Closing();
            _exit = true;
        }
    }
}