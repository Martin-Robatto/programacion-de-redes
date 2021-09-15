using System;
using System.Net.Sockets;
using System.Text;
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
            catch (Exception exception)
            {
                response.StatusCode = StatusCodeConstants.SERVER_ERROR;
            }
            return response;
        }
    }
}