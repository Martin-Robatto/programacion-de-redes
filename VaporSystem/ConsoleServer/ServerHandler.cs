using ConsoleServer.Function;
using Protocol;
using SettingsLogic;
using SettingsLogic.Interface;
using SocketLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using FunctionInterface;

namespace ConsoleServer
{
    public class ServerHandler
    {
        private bool _exit;
        
        private Socket _serverSocket;
        private List<Socket> _clientSockets = new List<Socket>();

        private Dictionary<int, IServerFunction> _actualFunctions;
        private readonly ISettingsManager _settingsManager = new SettingsManager();
        private NetworkManager _networkManager = new NetworkManager();
        private const string SHUTDOWN_SERVER = "0";

        public ServerHandler() { }

        public void Run()
        {
            _exit = false;
            try
            {
                Initialize();
                var thread = new Thread(() => ListenForConnections());
                thread.IsBackground = true;
                thread.Start();
                ServerDisplay.Listening();
                ServerDisplay.Menu();
                while (!_exit)
                {
                    var userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case SHUTDOWN_SERVER:
                            ShutDown();
                            break;
                        default:
                            ServerDisplay.InvalidOption();
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void Initialize()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var serverIpAddress = _settingsManager.ReadSetting(ServerConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ServerConfig.ServerPortConfigKey);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            ServerDisplay.Starting(serverIpAddress, serverPort);
            _serverSocket.Bind(ipEndPoint);
        }

        private void ListenForConnections()
        {
            _serverSocket.Listen(100);
            while (!_exit)
            {
                var clientSocket = _serverSocket.Accept();
                _clientSockets.Add(clientSocket);
                ServerDisplay.NewConnection();
                var thread = new Thread(() => Handle(clientSocket));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void Handle(Socket clientSocket)
        {
            var isClientConnected = true;
            try
            {
                while (isClientConnected && !_exit)
                {
                    var buffer = _networkManager.Receive(clientSocket, HeaderConstants.HEADER_LENGTH);
                    var header = new Header(buffer);
                    if (header.Command == FunctionConstants.EXIT)
                    {
                        isClientConnected = false;
                        ServerDisplay.ConnectionInterrupted();
                        _clientSockets.Remove(clientSocket);
                    }
                    ExecuteFunction(clientSocket, header);
                }
            }
            catch (IOException)
            {
                ServerDisplay.ConnectionInterrupted();
                _clientSockets.Remove(clientSocket);
            }
            catch (SocketException)
            {
                ServerDisplay.ConnectionInterrupted();
                _clientSockets.Remove(clientSocket);
            }
        }

        private void ExecuteFunction(Socket socket, Header header)
        {
            _actualFunctions = FunctionDictionary.Get();
            var command = _actualFunctions[header.Command];
            command.Execute(socket, header: header);
        }

        private void ShutDown()
        {
            ServerDisplay.Closing();
            foreach (var client in _clientSockets)
            {
                client.Shutdown(SocketShutdown.Both);
            }
            _clientSockets.Clear();
            _serverSocket.Close();
            _exit = true;
        }
    }
}