using System.Net.Sockets;
using Protocol;

namespace ConsoleClient.Function
{
    public abstract class FunctionTemplate
    {
        public void Execute(Socket socket, Header header = null)
        {
            var dataPacket = BuildRequest();
            SendRequest(socket, dataPacket);
            ReceiveResponse(socket, header);
        }

        public abstract DataPacket BuildRequest();
        public abstract void SendRequest(Socket socket, DataPacket dataPacket);
        public abstract void ReceiveResponse(Socket socket, Header header = null);

    }
}