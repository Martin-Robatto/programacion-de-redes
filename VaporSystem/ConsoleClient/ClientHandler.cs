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
        private static bool _exit;
        private static bool _connected;
        private static TcpClient _tcpClient;
        private static string _actualSession = string.Empty;
        private static Dictionary<int, IClientFunction> _actualFunctions;
        private static readonly ISettingsManager _settingsManager = new SettingsManager();

        public static void Run()
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

        private static void Initialize()
        {
            var clientIpAddress = _settingsManager.ReadSetting(ClientConfig.ClientIpConfigKey);
            var clientPort = GetPort();
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(clientIpAddress), clientPort);
            _tcpClient = new TcpClient(ipEndPoint);
        }

        private static int GetPort()
        {
            var random = new Random();
            var port = random.Next(20001, 30001);
            return port;
        }

        private static void Connect()
        {
            ClientDisplay.Connecting();
            var serverIpAddress = _settingsManager.ReadSetting(ClientConfig.ServerIpConfigKey);
            var serverPort = _settingsManager.ReadSetting(ClientConfig.ServerPortConfigKey);
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), int.Parse(serverPort));
            _tcpClient.Connect(ipEndPoint);
            _connected = true;
            ClientDisplay.Connected();
        }

        private static void DisplayServerUpMenu(NetworkStream networkStream)
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

        private static void DisplayServerDownMenu()
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

        public static void SetActualSession(string user)
        {
            _actualSession = user;
            SetActualFunctions(FunctionDictionary.Main());
        }

        public static void SetActualFunctions(Dictionary<int, IClientFunction> functions)
        {
            _actualFunctions = functions;
        }

        public static void ShutDown()
        {
            _tcpClient.Close();
            _exit = true;
        }
    }
}