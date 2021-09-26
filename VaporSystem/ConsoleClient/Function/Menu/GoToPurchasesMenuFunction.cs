﻿using System.Net.Sockets;
using FunctionInterface;

namespace ConsoleClient.Function.Menu
{
    public class GoToPurchasesMenuFunction : IClientFunction
    {
        public string Name { get; set; }
        public void Execute(NetworkStream stream, string session = null)
        {
            ClientHandler.SetActualFunctions(FunctionDictionary.Purchases());
        }

        public GoToPurchasesMenuFunction()
        {
            Name = "Tienda";
        }
    }
}