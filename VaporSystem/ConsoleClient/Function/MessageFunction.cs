using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Protocol;
using SocketLogic;

namespace ConsoleClient.Function
{
    public class MessageFunction : FunctionTemplate
    {
        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el mensaje:");
            var message = Console.ReadLine();
            var header = new Header(HeaderConstants.Request, FunctionConstants.Message, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void ProcessResponse(byte[] bufferData)
        {
            throw new NotImplementedException();
        }
    }
}