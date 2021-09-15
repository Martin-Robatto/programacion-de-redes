using System;
using System.Net.Sockets;
using Protocol;
using SocketLogic;

namespace ConsoleServer.Function
{
    public abstract class FunctionTemplate 
    {
        public void Execute(NetworkStream stream, Header header = null)
        {
            var bufferData = new byte[header.DataLength];
            SocketManager.Receive(stream, header.DataLength, bufferData);
            var response = ProcessRequest(bufferData);
            var dataPacket = BuildResponse(response);
            SocketManager.Send(stream, dataPacket);
        }

        public abstract ResponseData ProcessRequest(byte[] bufferData);
        
        public virtual DataPacket BuildResponse(ResponseData responseData)
        {
            var message = responseData.Data;
            var header = new Header(HeaderConstants.RESPONSE, responseData.Function, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message,
                StatusCode = responseData.StatusCode
            };
        }
    }
}