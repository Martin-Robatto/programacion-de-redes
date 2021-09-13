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
        public override void ProcessRequest(byte[] bufferData)
        {
            var userLine = Encoding.UTF8.GetString(bufferData);
            UserService.Instance.Register(userLine);
        }

        public override DataPacket BuildResponse()
        {
            var message = StatusCodeConstants.Created;
            var header = new Header(HeaderConstants.Response, FunctionConstants.Register, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }
    }
}