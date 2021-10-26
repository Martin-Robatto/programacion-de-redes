using FileLogic;
using Protocol;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketLogic
{
    public class NetworkManager
    {

        private FileManager _fileManager = new FileManager();
        private FileStreamManager _fileStreamManager = new FileStreamManager(); 
        
        public async Task SendAsync(Socket socket, DataPacket dataPacket)
        {
            var header = dataPacket.Header.GetRequest();
            await SendDataAsync(socket, header);

            var message = Encoding.UTF8.GetBytes(dataPacket.Payload);
            await SendDataAsync(socket, message);

            if (dataPacket.StatusCode != StatusCodeConstants.EMPTY)
            {
                var statusCode = Encoding.UTF8.GetBytes(dataPacket.StatusCode.ToString());
                await SendDataAsync(socket, statusCode);
            }
        }

        private async Task SendDataAsync(Socket socket, byte[] data)
        {
            int offset = 0;
            int size = data.Length;
            while (offset < size)
            {
                int sent = await socket.SendAsync(data, SocketFlags.None);
                if (sent == 0)
                {
                    throw new SocketException();
                }
                offset += sent;
            }
        }

        public async Task<byte[]> ReceiveAsync(Socket socket, int length)
        {
            var offset = 0;
            byte[] buffer = new byte[length];
            while (offset < length)
            {
                var received = await socket.ReceiveAsync(buffer, SocketFlags.None);
                if (received == 0)
                {
                    throw new SocketException();
                }
                offset += received;
            }
            return buffer;
        }

        public async Task UploadFileAsync(Socket socket, long size, string filePath)
        {
            long offset = 0;

            long parts = _fileManager.GetParts(size);
            long currentPart = 1;

            while (offset < size)
            {
                byte[] data;
                if (currentPart == parts)
                {
                    var lastPartSize = (int)(size - offset);
                    data = await _fileStreamManager.ReadAsync(filePath, offset, lastPartSize);
                    offset += lastPartSize;
                }
                else
                {
                    data = await _fileStreamManager.ReadAsync(filePath, offset, HeaderConstants.MAX_PACKET_SIZE);
                    offset += HeaderConstants.MAX_PACKET_SIZE;
                }
                await SendDataAsync(socket, data);
                currentPart++;
            }
        }

        public async Task DownloadFileAsync(Socket socket, long size, string fileName)
        {
            long offset = 0;

            long parts = _fileManager.GetParts(size);
            long currentPart = 1;

            while (offset < size)
            {
                byte[] data;
                if (currentPart == parts)
                {
                    var lastPartSize = (int)(size - offset);
                    data = await ReceiveAsync(socket, lastPartSize);
                    offset += lastPartSize;
                }
                else
                {
                    data = await ReceiveAsync(socket, HeaderConstants.MAX_PACKET_SIZE);
                    offset += HeaderConstants.MAX_PACKET_SIZE;
                }

                await _fileStreamManager.WriteAsync(fileName, data);
                currentPart++;
            }
        }
    }
}