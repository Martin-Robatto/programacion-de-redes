using Protocol;
using SocketLogic;
using System.Net.Sockets;
using FunctionInterface;

namespace ConsoleServer.Function
{
    public abstract class FunctionTemplate : IServerFunction
    {
        protected NetworkStream networkStream;
        public void Execute(NetworkStream stream, Header header = null)
        {
            networkStream = stream;
            var bufferData = ReceiveRequest(header);
            var response = ProcessRequest(bufferData);
            var dataPacket = BuildResponse(response);
            SendResponse(dataPacket);
        }

        public virtual byte[] ReceiveRequest(Header header)
        {
            return NetworkStreamManager.Receive(networkStream, header.DataLength);
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

        public virtual void SendResponse(DataPacket dataPacket)
        {
            NetworkStreamManager.Send(networkStream, dataPacket);
        }
    }
}