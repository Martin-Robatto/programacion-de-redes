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
            ProcessRequest(bufferData);
            var dataPacket = BuildResponse();
            SendResponse(socket, dataPacket);
        }

        public virtual byte[] ReceiveRequest(Socket socket, Header header = null)
        {
            var bufferData = new byte[header.DataLength];  
            SocketManager.Receive(socket, header.DataLength, bufferData);
            return bufferData;
        }
        
        public abstract void ProcessRequest(byte[] bufferData);
        public abstract DataPacket BuildResponse();

        public virtual void SendResponse(Socket socket, DataPacket dataPacket = null)
        {
            SocketManager.Send(socket, dataPacket.Header, dataPacket.Payload);
        }
    }
}