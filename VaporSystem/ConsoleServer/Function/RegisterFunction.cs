using System;
using System.Net.Sockets;
using System.Text;
using Protocol;
using Service;
using SocketLogic;

namespace ConsoleServer.Function
{
    public class RegisterFunction : FunctionTemplate
    {
        public override void ReceiveRequest(Socket socket, Header header = null)
        {
            var bufferData = new byte[header.DataLength];  
            SocketManager.Receive(socket, header.DataLength, bufferData);
            var userLine = Encoding.UTF8.GetString(bufferData);
            UserService.Instance.Register(userLine);
        }

        public override DataPacket BuildResponse()
        {
            var message = "Created";
            var header = new Header(HeaderConstants.Response, FunctionConstants.Register, message.Length);
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