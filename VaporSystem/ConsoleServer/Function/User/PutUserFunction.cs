using System;
using System.Text;
using ConsoleServer.Logs;
using Domain;
using Exceptions;
using Protocol;
using Service;

namespace ConsoleServer.Function
{
    public class PutUserFunction: FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            base.function = FunctionConstants.PUT_USER;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                UserService.Instance.Update(userLine);
                base.statusCode = StatusCodeConstants.OK;
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
            string[] attributes = userLine.Split("&");
            string[] userAttributes = attributes[1].Split("#");
            Log newLog = new Log()
            {
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                User = $"{attributes[0]} --> {userAttributes[0]}",
                Game = string.Empty,
                Action = "Put User",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}