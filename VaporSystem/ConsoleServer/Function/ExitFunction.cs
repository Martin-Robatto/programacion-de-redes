using System;
using Exceptions;
using Protocol;

namespace ConsoleServer.Function
{
    public class ExitFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.EXIT;
            try
            {
                response.Data = string.Empty;
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