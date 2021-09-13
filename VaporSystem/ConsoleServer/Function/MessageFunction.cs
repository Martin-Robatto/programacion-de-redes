using System;
using System.Net.Sockets;
using System.Text;
using Protocol;
using SocketLogic;

namespace ConsoleServer.Function
{
    public class MessageFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            Console.WriteLine("Mensaje: " + Encoding.UTF8.GetString(bufferData));
        }

        public override DataPacket BuildResponse()
        {
            var message = StatusCodeConstants.Ok;
            var header = new Header(HeaderConstants.Response, FunctionConstants.Message, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }
    }
}