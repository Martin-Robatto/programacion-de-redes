using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using ConsoleServer.LogsLogic;
using Domain;

namespace ConsoleServer.Function
{
    public class GetAllGamesFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_ALL_GAMES;
            try
            {
                base.data = GameService.Instance.GetGames();
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
                Action = "Get All Games",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}
