using System;
using System.Net.Sockets;
using System.Text;
using FunctionInterface;
using Protocol;
using SocketLogic;

namespace ConsoleClient.Function
{
    public abstract class FunctionTemplate : IClientFunction
    {
        public string Name { get; set; }

        public void Execute(NetworkStream stream, string session = null)
        {
            var dataPacket = BuildRequest(session);
            SocketManager.Send(stream, dataPacket);
            var bufferData = ReceiveResponse(stream);
            ProcessResponse(bufferData);
        }

        public abstract DataPacket BuildRequest(string session);

        public virtual byte[] ReceiveResponse(NetworkStream stream)
        {
            try
            {
                var bufferHeader = SocketManager.Receive(stream, HeaderConstants.HEADER_LENGTH);
                var header = new Header(bufferHeader);
                var bufferData = SocketManager.Receive(stream, header.DataLength);
                var bufferStatusCode = SocketManager.Receive(stream, HeaderConstants.STATUS_CODE_LENGTH);
                
                var bufferToReturn = new byte[header.DataLength + HeaderConstants.STATUS_CODE_LENGTH];
                Array.Copy(bufferStatusCode, 0, bufferToReturn, 0, HeaderConstants.STATUS_CODE_LENGTH);
                Array.Copy(bufferData, 0, bufferToReturn, HeaderConstants.STATUS_CODE_LENGTH, header.DataLength);
                
                return bufferToReturn;
            }
            catch (Exception exception)
            {
                return Encoding.UTF8.GetBytes(exception.Message);
            }
        }
        public abstract void ProcessResponse(byte[] bufferData);
    }
}