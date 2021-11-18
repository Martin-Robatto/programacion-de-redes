using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using ConsoleServer.Logs;
using Domain;

namespace ConsoleServer.Function
{
    public class GetPublishesByUserFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_PUBLISHES_BY_USER;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                base.data = PublishService.Instance.Get(gameLine);
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
            Log newLog = new Log()
            {
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                User = gameLine,
                Game = string.Empty,
                Action = "Get Publishes By User",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}