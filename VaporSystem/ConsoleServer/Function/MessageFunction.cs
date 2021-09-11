using System;
using System.Net.Sockets;
using System.Text;
using Protocol;
using SocketLogic;

namespace ConsoleServer.Function
{
    public class MessageFunction : FunctionTemplate
    {
        public override void ReceiveRequest(Socket socket, Header header = null)
        {
            var bufferData = new byte[header.DataLength];  
            SocketManager.Receive(socket, header.DataLength, bufferData);
            Console.WriteLine("Mensaje: " + Encoding.UTF8.GetString(bufferData));
        }

        public override DataPacket BuildResponse()
        {
            var message = "Ok";
            var header = new Header(HeaderConstants.Response, FunctionConstants.Message, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void SendResponse(Socket socket, DataPacket dataPacket = null)
        {
            SocketManager.Send(socket, dataPacket.Header, dataPacket.Payload);
        }
    }
}