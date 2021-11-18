using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using ConsoleServer.Logs;
using Domain;

namespace ConsoleServer.Function.Review
{
    public class GetReviewsByUserFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_REVIEWS_BY_USER;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                base.data = ReviewService.Instance.GetByUser(userLine);
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
            Log newLog = new Log()
            {
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                User = reviewLine,
                Game = string.Empty,
                Action = "Get Reviews By User",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}