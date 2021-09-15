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
        public const string NAME = "Mensaje";
        
        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el mensaje:");
            var message = Console.ReadLine();
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.MESSAGE, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void ProcessResponse(byte[] bufferData)
        {
            var statusCode = Int32.Parse(Encoding.UTF8.GetString(bufferData, 0, HeaderConstants.STATUS_CODE_LENGTH));
            if (statusCode == StatusCodeConstants.SERVER_ERROR)
            {
                Console.WriteLine("Error de servidor");
            }
        }

        public MessageFunction()
        {
            base.Name = NAME;
        }
    }
}