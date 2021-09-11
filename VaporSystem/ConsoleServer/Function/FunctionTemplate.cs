using System.Net.Sockets;
using Protocol;

namespace ConsoleServer.Function
{
    public abstract class FunctionTemplate
    {
        public void Execute(Socket socket, Header header = null)
        {
            ReceiveRequest(socket, header);
            var dataPacket = BuildResponse();
            SendResponse(socket, dataPacket);
        }

        public abstract void ReceiveRequest(Socket socket, Header header = null);
        public abstract DataPacket BuildResponse();
        public abstract void SendResponse(Socket socket, DataPacket dataPacket = null);
    }
}