﻿using System;
using System.Net.Sockets;
using System.Text;
using Protocol;
using SocketLogic;

namespace ConsoleServer.Function
{
    public class MessageFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.Message;
            try
            {
                Console.WriteLine("Mensaje: " + Encoding.UTF8.GetString(bufferData));
                response.StatusCode = StatusCodeConstants.Ok;
            }
            catch (Exception exception)
            {
                response.StatusCode = StatusCodeConstants.ServerError;
            }
            return response;
        }
    }
}