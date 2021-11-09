using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using Domain;

namespace ConsoleServer.Function.Review
{
    public class PostReviewFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.POST_REVIEW;
            try
            {
                var reviewLine = Encoding.UTF8.GetString(bufferData);
                ReviewService.Instance.Save(reviewLine);
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
            var reviewLine = Encoding.UTF8.GetString(bufferData);
            string[] attributes = reviewLine.Split("&");
            Log newLog = new Log()
            {
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                User = attributes[0],
                Game = attributes[1],
                Action = "Post Review",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}