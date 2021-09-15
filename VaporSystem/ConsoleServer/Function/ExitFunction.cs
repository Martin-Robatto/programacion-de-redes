using System;
using Protocol;

namespace ConsoleServer.Function
{
    public class ExitFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.GET_ALL_GAMES;
            try
            {
                response.Data = string.Empty;
                response.StatusCode = StatusCodeConstants.OK;
            }
            catch (Exception exception)
            {
                response.StatusCode = StatusCodeConstants.SERVER_ERROR;
            }
            return response;
        }
    }
}