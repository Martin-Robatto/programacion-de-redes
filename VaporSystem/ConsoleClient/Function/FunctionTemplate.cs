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
        protected NetworkManager networkManager = new NetworkManager();
        protected Socket socket;
        protected string session;

        public void Execute(Socket socket, string session = null)
        {
            this.socket = socket;
            this.session = session;
            var dataPacket = BuildRequest();
            SendRequest(dataPacket);
            var bufferData = ReceiveResponse();
            ProcessResponse(bufferData);
        }

        public abstract DataPacket BuildRequest();

        public virtual void SendRequest(DataPacket dataPacket)
        {
            networkManager.Send(socket, dataPacket);
        }

        public virtual byte[] ReceiveResponse()
        {
            try
            {
                var bufferHeader = networkManager.Receive(socket, HeaderConstants.HEADER_LENGTH);
                var header = new Header(bufferHeader);
                var bufferData = networkManager.Receive(socket, header.DataLength);
                var bufferStatusCode = networkManager.Receive(socket, HeaderConstants.STATUS_CODE_LENGTH);

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