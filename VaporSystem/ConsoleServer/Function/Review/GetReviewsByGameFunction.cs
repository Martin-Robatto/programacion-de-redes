using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using Domain;

namespace ConsoleServer.Function.Review
{
    public class GetReviewsByGameFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_REVIEWS_BY_GAME;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                base.data = ReviewService.Instance.GetByGame(gameLine);
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
            var reviewLine = Encoding.UTF8.GetString(bufferData);
            string[] attributes = reviewLine.Split("&");
            Log newLog = new Log()
            {
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                User = attributes[0],
                Game = attributes[1],
                Action = "Get Reviews By Game",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}