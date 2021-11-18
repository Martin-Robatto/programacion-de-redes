﻿using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using ConsoleServer.Logs;
using Domain;

namespace ConsoleServer.Function
{
    public class PostUserFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            base.function = FunctionConstants.POST_USER;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                UserService.Instance.Register(userLine);
                base.statusCode = StatusCodeConstants.CREATED;
            }
            catch (AppException exception)
            {
                base.data = exception.Message;
                base.statusCode = exception.StatusCode;
            }
            catch (Exception)
            {
                base.data = "Error de servidor";
                base.statusCode = StatusCodeConstants.SERVER_ERROR;
            }
        }

        public override void SendLog(byte[] bufferData)
        {
            var userLine = Encoding.UTF8.GetString(bufferData);
            string[] attributes = userLine.Split("#");
            Log newLog = new Log()
            {
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                User = attributes[0],
                Game = string.Empty,
                Action = "Post User",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}