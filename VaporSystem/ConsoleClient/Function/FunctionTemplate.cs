using System;
using System.Net.Sockets;
using System.Text;
using Protocol;
using SocketLogic;

namespace ConsoleClient.Function
{
    public abstract class FunctionTemplate
    {
        public void Execute(Socket socket, Header header = null)
        {
            var dataPacket = BuildRequest();
            SendRequest(socket, dataPacket);
            var bufferData = ReceiveResponse(socket, header);
            ProcessResponse(bufferData);
        }

        public abstract DataPacket BuildRequest();

        public virtual void SendRequest(Socket socket, DataPacket dataPacket)
        {
            SocketManager.Send(socket, dataPacket);
        }

        public virtual byte[] ReceiveResponse(Socket socket, Header header = null)
        {
            var bufferHeader = new byte[HeaderConstants.HeaderLength];
            try
            {
                SocketManager.Receive(socket, HeaderConstants.HeaderLength, bufferHeader);
                header = new Header();
                header.DecodeData(bufferHeader);
                var bufferData = new byte[header.DataLength];
                SocketManager.Receive(socket, header.DataLength, bufferData);
                return bufferData;
            }
            catch (Exception exception)
            {
                return Encoding.UTF8.GetBytes(exception.Message);
            }
        }
        public abstract void ProcessResponse(byte[] bufferData);
    }
}