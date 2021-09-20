using System;
using System.Text;
using Exceptions;
using Protocol;
using Service;

namespace ConsoleServer.Function
{
    public class PostPublishFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.POST_PUBLISH;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                PublishService.Instance.Save(gameLine);
                response.StatusCode = StatusCodeConstants.CREATED;
            }
            catch (AppException exception)
            {
                response.Data = exception.Message;
                response.StatusCode = exception.StatusCode;
            }
            catch (Exception exception)
            {
                response.Data = "Error de servidor";
                response.StatusCode = StatusCodeConstants.SERVER_ERROR;
            }
            return response;
        }
    }
}