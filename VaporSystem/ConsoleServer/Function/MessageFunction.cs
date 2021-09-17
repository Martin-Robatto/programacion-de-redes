using System;
using System.Net.Sockets;
using System.Text;
using Exceptions;
using Protocol;
using SocketLogic;

namespace ConsoleServer.Function
{
    public class MessageFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.MESSAGE;
            try
            {
                Console.WriteLine("Mensaje: " + Encoding.UTF8.GetString(bufferData));
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