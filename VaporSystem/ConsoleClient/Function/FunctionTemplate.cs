using System;
using System.Net.Sockets;
using System.Text;
using FunctionInterface;
using Protocol;
using SocketLogic;

namespace ConsoleClient.Function
{
    public abstract class FunctionTemplate : IFunction
    {
        public string Name { get; set; }

        public void Execute(NetworkStream stream, Header header = null)
        {
            var dataPacket = BuildRequest();
            SocketManager.Send(stream, dataPacket);
            var bufferData = ReceiveResponse(stream);
            ProcessResponse(bufferData);
        }

        public abstract DataPacket BuildRequest();

        public virtual byte[] ReceiveResponse(NetworkStream stream)
        {
            try
            {
                var bufferHeader = new byte[HeaderConstants.HEADER_LENGTH];
                SocketManager.Receive(stream, HeaderConstants.HEADER_LENGTH, bufferHeader);
                var header = new Header(bufferHeader);
                
                var bufferData = new byte[header.DataLength];
                SocketManager.Receive(stream, header.DataLength, bufferData);
                
                var bufferStatusCode = new byte[HeaderConstants.STATUS_CODE_LENGTH];
                SocketManager.Receive(stream, HeaderConstants.STATUS_CODE_LENGTH, bufferStatusCode);
                
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