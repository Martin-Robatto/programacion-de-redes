using Protocol;
using SocketLogic;
using System.Net.Sockets;
using FunctionInterface;

namespace ConsoleServer.Function
{
    public abstract class FunctionTemplate : IServerFunction
    {
        protected Socket socket;
        protected NetworkManager networkManager = new NetworkManager();

        protected string data = string.Empty;
        protected int function;
        protected int statusCode;
        
        public void Execute(Socket socket, Header header = null)
        {
            this.socket = socket;
            var bufferData = ReceiveRequest(header);
            ProcessRequest(bufferData);
            var dataPacket = BuildResponse();
            SendResponse(dataPacket);
        }

        public virtual byte[] ReceiveRequest(Header header)
        {
            return networkManager.Receive(socket, header.DataLength);
        }

        public abstract void ProcessRequest(byte[] bufferData);

        public virtual DataPacket BuildResponse()
        {
            var message = data;
            var header = new Header(HeaderConstants.RESPONSE, function, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message,
                StatusCode = statusCode
            };
        }

        public virtual void SendResponse(DataPacket dataPacket)
        {
            networkManager.Send(socket, dataPacket);
        }
    }
}