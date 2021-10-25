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
using System.Threading.Tasks;
using FunctionInterface;

namespace ConsoleServer
{
    public class ServerHandler
    {
        private bool _exit;
        
        private Socket _serverSocket;
        private List<Socket> _clientSockets = new List<Socket>();

        private FunctionDictionary _functionDictionary = new FunctionDictionary();
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
                var task = Task.Run(async () => await ListenForConnectionsAsync().ConfigureAwait(false));
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
            _serverSocket.Listen(100);
        }

        private async Task ListenForConnectionsAsync()
        {
            while (!_exit)
            {
                var clientSocket = await _serverSocket.AcceptAsync().ConfigureAwait(false);
                _clientSockets.Add(clientSocket);
                ServerDisplay.NewConnection();
                var task = Task.Run(async () => await HandleAsync(clientSocket).ConfigureAwait(false));
            }
        }

        private async Task HandleAsync(Socket clientSocket)
        {
            var isClientConnected = true;
            try
            {
                while (isClientConnected && !_exit)
                {
                    var buffer = await _networkManager.ReceiveAsync(clientSocket, HeaderConstants.HEADER_LENGTH);
                    var header = new Header(buffer);
                    if (header.Command == FunctionConstants.EXIT)
                    {
                        isClientConnected = false;
                        ServerDisplay.ConnectionInterrupted();
                        _clientSockets.Remove(clientSocket);
                    }
                    await ExecuteFunctionAsync(clientSocket, header);
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

        private async Task ExecuteFunctionAsync(Socket socket, Header header)
        {
            _actualFunctions = _functionDictionary.Get();
            var command = _actualFunctions[header.Command];
            await command.ExecuteAsync(socket, header: header);
        }

        private void ShutDown()
        {
            ServerDisplay.Closing();
            _serverSocket.Close(0);
            foreach (var client in _clientSockets)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            _exit = true;
        }
    }
}