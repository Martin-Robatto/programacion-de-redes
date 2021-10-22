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
        private string _actualSession;

        private Socket _clientSocket;

        private Dictionary<int, IClientFunction> _actualFunctions;
        private readonly ISettingsManager _settingsManager = new SettingsManager();

        private static ClientHandler _instance;

        public static ClientHandler Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new ClientHandler();
                }
                return _instance;
            }
        }

        private ClientHandler()
        {
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
                    while (!_exit)
                    {
                        DisplayServerUpMenu();
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
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  
            var clientIpAddress = _settingsManager.ReadSetting(ClientConfig.ClientIpConfigKey);
            var clientPort = GetPort();
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(clientIpAddress), clientPort);
            _clientSocket.Bind(ipEndPoint);
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
            _clientSocket.Connect(ipEndPoint);
            _connected = true;
            ClientDisplay.Connected();
        }

        private void DisplayServerUpMenu()
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
                command.Execute(_clientSocket, session: _actualSession);
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
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
            _exit = true;
        }
    }
}