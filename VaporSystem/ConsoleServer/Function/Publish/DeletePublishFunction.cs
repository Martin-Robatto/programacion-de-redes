using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using ConsoleServer.LogsLogic;
using Domain;

namespace ConsoleServer.Function
{
    public class DeletePublishFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.DELETE_PUBLISH;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                PublishService.Instance.Delete(gameLine);
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
            var gameLine = Encoding.UTF8.GetString(bufferData);
            string[] attributes = gameLine.Split("&");
            Log newLog = new Log()
            {
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                User = attributes[0],
                Game = attributes[1],
                Action = "Delete Publish",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}