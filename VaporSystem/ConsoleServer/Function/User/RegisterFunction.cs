using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;

namespace ConsoleServer.Function
{
    public class RegisterFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            base.function = FunctionConstants.REGISTER;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                UserService.Instance.Register(userLine);
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