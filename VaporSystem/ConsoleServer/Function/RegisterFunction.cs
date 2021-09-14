using Protocol;
using Service;
using System;
using System.Text;

namespace ConsoleServer.Function
{
    public class RegisterFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.Register;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                UserService.Instance.Register(userLine);
                response.StatusCode = StatusCodeConstants.Created;
            }
            catch (Exception exception)
            {
                response.StatusCode = StatusCodeConstants.ServerError;
            }
            return response;
        }
    }
}