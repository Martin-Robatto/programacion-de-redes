using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;

namespace ConsoleServer.Function
{
    public class GetPublishesByUserFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_PUBLISHES_BY_USER;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                base.data = PublishService.Instance.Get(userLine);
                base.statusCode = StatusCodeConstants.OK;
            }
            catch (AppException exception)
            {
                base.data = exception.Message;
                base.statusCode = exception.StatusCode;
            }
            catch (Exception exception)
            {
                base.data = "Error de servidor";
                base.statusCode = StatusCodeConstants.SERVER_ERROR;
            }
            
        }
    }
}