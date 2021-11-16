using Protocol;
using SocketLogic;
using System.Net.Sockets;
using System.Threading.Tasks;
using FunctionInterface;

namespace ConsoleServer.Function
{
    public abstract class FunctionTemplate : IServerFunction
    {
        protected Socket socket;
        protected NetworkManager networkManager = new NetworkManager();

        protected string data = string.Empty;
        protected int function;
        public int statusCode { get; set; }
        
        protected string fileName = string.Empty;
        protected long fileSize = 0;
        
        public async Task ExecuteAsync(Socket socket, Header header = null)
        {
            this.socket = socket;
            var bufferData = await ReceiveRequestAsync(header);
            ProcessRequest(bufferData);
            if (!string.IsNullOrEmpty(fileName))
            {
                await DownloadFileAsync();
            }
            var dataPacket = BuildResponse();
            await SendResponseAsync(dataPacket);
            SendLog(bufferData);
        }

        public virtual async Task<byte[]> ReceiveRequestAsync(Header header)
        {
            return await networkManager.ReceiveAsync(socket, header.DataLength);
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

        public virtual async Task SendResponseAsync(DataPacket dataPacket)
        {
            await networkManager.SendAsync(socket, dataPacket);
        }
        
        public virtual async Task DownloadFileAsync()
        {
            await networkManager.DownloadFileAsync(socket, fileSize, fileName);
        }
        
        public abstract void SendLog(byte[] bufferData);
    }
}