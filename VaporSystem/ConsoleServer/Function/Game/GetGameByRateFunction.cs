using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using Domain;

namespace ConsoleServer.Function
{
    public class GetGameByRateFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_GAME_BY_RATE;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                base.data = GameService.Instance.GetByRate(gameLine);
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
                Game = null,
                Action = "Get Game By Rate",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}