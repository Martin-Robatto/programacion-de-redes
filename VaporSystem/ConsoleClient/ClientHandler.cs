using ConsoleClient.Function;
using FunctionInterface;
using SettingsLogic;
using SettingsLogic.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ConsoleClient
{
    public class ClientHandler
    {
        private bool _exit;
        private bool _connected;
        private TcpClient _tcpClient;
        private string _actualSession;
        private Dictionary<int, IClientFunction> _actualFunctions;
        private readonly ISettingsManager _settingsManager = new SettingsManager();
        private static ClientHandler _instance;
        private static readonly object _lock = new object();
        public static ClientHandler Instance
        {
            get { return GetInstance(); }
        }

        private ClientHandler() { }
        
        private static ClientHandler GetInstance()
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance = new ClientHandler();
                }
            }
            return _instance;
        }
        
        public void Run()
        {
            _exit = false;
            _connected = false;
            while (!_connected && !_exit)
            {
                try
                {
                    ClientDisplay.ClearConsole();
                    Initialize();
                    Connect();
                    _actualSession = string.Empty;
                    using (var networkStream = _tcpClient.GetStream())
                    {
                        while (!_exit)
                        {
                            DisplayServerUpMenu(networkStream);
                        }
                    }
                }
                catch (IOException)
                {
                    _connected = false;
                    _actualFunctions = FunctionDictionary.NoConnection();
                    DisplayServerDownMenu();
                }
                catch (SocketException)
                {
                    _connected = false;
                    _actualFunctions = FunctionDictionary.NoConnection();
                    DisplayServerDownMenu();
                }
            }
        }

        private void Initialize()
        {
            var clientIpAddress = _settingsManager.ReadSetting(ClientConfig.ClientIpConfigKey);
            var clientPort = GetPort();
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(clientIpAddress), clientPort);
            _tcpClient = new TcpClient(ipEndPoint);
        }

        private int GetPort()
        {
            var random = new Random();
            var port = random.Next(20001, 30001);
            return port;
        }

        private void Connect()
        {
            ClientDisplay.Connecting();
            var serverIpAddress = _settingsManager.ReadSetting(ClientConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ClientConfig.ServerPortConfigKey);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            _tcpClient.Connect(ipEndPoint);
            _connected = true;
            ClientDisplay.Connected();
        }

        private void DisplayServerUpMenu(NetworkStream networkStream)
        {
            if (String.IsNullOrEmpty(_actualSession))
            {
                _actualFunctions = FunctionDictionary.LogIn();
            }
            ClientDisplay.Menu(_actualFunctions);
            ClientDisplay.ChooseOption();
            var userInput = Console.ReadLine();
            int input = int.TryParse(userInput, out input) ? input : 1000;
            if (_actualFunctions.ContainsKey(input))
            {
                var command = _actualFunctions[input];
                command.Execute(networkStream, session: _actualSession);
            }
            ClientDisplay.Continue();
            Console.ReadLine();
            ClientDisplay.ClearConsole();
        }

        private void DisplayServerDownMenu()
        {
            ClientDisplay.ConnectionInterrupted();
            ClientDisplay.Menu(_actualFunctions);
            ClientDisplay.Continue();
            ClientDisplay.ChooseOption();
            var userInput = Console.ReadLine();
            int input = int.TryParse(userInput, out input) ? input : 1000;
            if (_actualFunctions.ContainsKey(input))
            {
                var command = _actualFunctions[input];
                command.Execute();
            }
            ClientDisplay.ClearConsole();
        }

        public void SetActualSession(string user)
        {
            _actualSession = user;
            SetActualFunctions(FunctionDictionary.Main());
        }

        public void SetActualFunctions(Dictionary<int, IClientFunction> functions)
        {
            _actualFunctions = functions;
        }

        public void ShutDown()
        {
            _tcpClient.Close();
            _exit = true;
        }
    }
}