using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;

namespace ConsoleServer.Function.File
{
    public class PostFileFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.POST_FILE;
            try
            {
                var fileLine = Encoding.UTF8.GetString(bufferData);
                PublishService.Instance.DownloadPicture(base.socket, fileLine);
                base.statusCode = StatusCodeConstants.CREATED;
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