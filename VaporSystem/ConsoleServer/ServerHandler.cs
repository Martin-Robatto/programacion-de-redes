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
        private TcpListener _tcpListenerSocket;
        private Dictionary<int, IServerFunction> _functions;
        private Dictionary<TcpClient, NetworkStream> _clientSockets;
        private readonly ISettingsManager _settingsManager = new SettingsManager();
        private const string SHUTDOWN_SERVER = "0";

        public ServerHandler() { }

        public void Run()
        {
            _exit = false;
            try
            {
                Initialize();
                _clientSockets = new Dictionary<TcpClient, NetworkStream>();
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
            var serverIpAddress = _settingsManager.ReadSetting(ServerConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ServerConfig.ServerPortConfigKey);
            ServerDisplay.Starting(serverIpAddress, serverPort);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            _tcpListenerSocket = new TcpListener(ipEndPoint);
            _tcpListenerSocket.Start(100);
        }

        private void ListenForConnections()
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

        private void Handle(TcpClient tcpClientSocket, NetworkStream networkStream)
        {
            var isClientConnected = true;
            try
            {
                while (isClientConnected && !_exit)
                {
                    var buffer = NetworkStreamManager.Receive(networkStream, HeaderConstants.HEADER_LENGTH);
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

        private void ExecuteFunction(NetworkStream networkStream, Header header)
        {
            _functions = FunctionDictionary.Get();
            var command = _functions[header.Command];
            command.Execute(networkStream, header: header);
        }

        private void ShutDown()
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