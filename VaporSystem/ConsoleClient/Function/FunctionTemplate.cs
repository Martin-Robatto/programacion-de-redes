using FunctionInterface;
using Protocol;
using SocketLogic;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Function
{
    public abstract class FunctionTemplate : IClientFunction
    {
        public string Name { get; set; }
        protected NetworkManager networkManager = new NetworkManager();
        protected Socket socket;
        protected string session;

        protected string fileName = string.Empty;
        protected long fileSize = 0;

        public async Task ExecuteAsync(Socket socket, string session = null)
        {
            this.socket = socket;
            this.session = session;
            var dataPacket = BuildRequest();
            await SendRequestAsync(dataPacket);
            var bufferData = await ReceiveResponseAsync();
            ProcessResponse(bufferData);
            if (!string.IsNullOrEmpty(fileName))
            {
                await DownloadFileAsync();
            }
        }

        public abstract DataPacket BuildRequest();

        public virtual async Task SendRequestAsync(DataPacket dataPacket)
        {
            await networkManager.SendAsync(socket, dataPacket);
        }

        public virtual async Task<byte[]> ReceiveResponseAsync()
        {
            try
            {
                var bufferHeader = await networkManager.ReceiveAsync(socket, HeaderConstants.HEADER_LENGTH);
                var header = new Header(bufferHeader);
                var bufferData = await networkManager.ReceiveAsync(socket, header.DataLength);
                var bufferStatusCode = await networkManager.ReceiveAsync(socket, HeaderConstants.STATUS_CODE_LENGTH);

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

        public virtual async Task DownloadFileAsync()
        {
            await networkManager.DownloadFileAsync(socket, fileSize, fileName);
        }

    }
}