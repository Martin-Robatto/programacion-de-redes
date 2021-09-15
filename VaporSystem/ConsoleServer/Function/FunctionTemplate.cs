using System;
using System.Net.Sockets;
using Protocol;
using SocketLogic;

namespace ConsoleServer.Function
{
    public abstract class FunctionTemplate 
    {
        public void Execute(Socket socket, Header header = null)
        {
            var bufferData = ReceiveRequest(socket, header);
            var responseData = ProcessRequest(bufferData);
            var dataPacket = BuildResponse(responseData);
            SendResponse(socket, dataPacket);
        }

        public virtual byte[] ReceiveRequest(Socket socket, Header header = null)
        {
            var bufferData = new byte[header.DataLength];  
            SocketManager.Receive(socket, header.DataLength, bufferData);
            return bufferData;
        }
        
        public abstract ResponseData ProcessRequest(byte[] bufferData);
        public virtual DataPacket BuildResponse(ResponseData responseData)
        {
            var message = responseData.Data;
            var header = new Header(HeaderConstants.Response, responseData.Function, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message,
                StatusCode = responseData.StatusCode
            };
        }

        public virtual void SendResponse(Socket socket, DataPacket dataPacket = null)
        {
            SocketManager.Send(socket, dataPacket);
        }
    }
}