using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;

namespace ConsoleServer.Function
{
    public class LogInFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.LOGIN;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                response.Data = UserService.Instance.LogIn(userLine);
                response.StatusCode = StatusCodeConstants.OK;
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