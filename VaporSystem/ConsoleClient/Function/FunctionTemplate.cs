using FunctionInterface;
using Protocol;
using SocketLogic;
using System;
using System.Net.Sockets;
using System.Text;

namespace ConsoleClient.Function
{
    public abstract class FunctionTemplate : IClientFunction
    {
        public string Name { get; set; }
        protected NetworkStream networkStream;
        protected string session;

        public void Execute(NetworkStream stream, string session = null)
        {
            networkStream = stream;
            this.session = session;
            var dataPacket = BuildRequest();
            SendRequest(dataPacket);
            var bufferData = ReceiveResponse();
            ProcessResponse(bufferData);
        }

        public abstract DataPacket BuildRequest();

        public virtual void SendRequest(DataPacket dataPacket)
        {
            NetworkStreamManager.Send(networkStream, dataPacket);
        }

        public virtual byte[] ReceiveResponse()
        {
            try
            {
                var bufferHeader = NetworkStreamManager.Receive(networkStream, HeaderConstants.HEADER_LENGTH);
                var header = new Header(bufferHeader);
                var bufferData = NetworkStreamManager.Receive(networkStream, header.DataLength);
                var bufferStatusCode = NetworkStreamManager.Receive(networkStream, HeaderConstants.STATUS_CODE_LENGTH);

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