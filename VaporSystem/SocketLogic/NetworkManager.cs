using FileLogic;
using Protocol;
using System.Net.Sockets;
using System.Text;

namespace SocketLogic
{
    public class NetworkManager
    {
        public void Send(Socket socket, DataPacket dataPacket)
        {
            var header = dataPacket.Header.GetRequest();
            SendData(socket, header);

            var message = Encoding.UTF8.GetBytes(dataPacket.Payload);
            SendData(socket, message);

            if (dataPacket.StatusCode != StatusCodeConstants.EMPTY)
            {
                var statusCode = Encoding.UTF8.GetBytes(dataPacket.StatusCode.ToString());
                SendData(socket, statusCode);
            }
        }

        private void SendData(Socket socket, byte[] data)
        {
            int offset = 0;
            int size = data.Length;
            while (offset < size)
            {
                int sent = socket.Send(data, offset, size - offset, SocketFlags.None);
                if (sent == 0)
                {
                    throw new SocketException();
                }
                offset += sent;
            }
        }

        public byte[] Receive(Socket socket, int length)
        {
            var offset = 0;
            byte[] buffer = new byte[length];
            while (offset < length)
            {
                var received = socket.Receive(buffer, offset, length - offset, SocketFlags.None);
                if (received == 0)
                {
                    throw new SocketException();
                }
                offset += received;
            }
            return buffer;
        }

        public void UploadFile(Socket socket, long size, string filePath)
        {
            long offset = 0;

            long parts = FileManager.GetParts(size);
            long currentPart = 1;

            while (offset < size)
            {
                byte[] data;
                if (currentPart == parts)
                {
                    var lastPartSize = (int)(size - offset);
                    data = FileStreamManager.Read(filePath, offset, lastPartSize);
                    offset += lastPartSize;
                }
                else
                {
                    data = FileStreamManager.Read(filePath, offset, HeaderConstants.MAX_PACKET_SIZE);
                    offset += HeaderConstants.MAX_PACKET_SIZE;
                }
                SendData(socket, data);
                currentPart++;
            }
        }

        public void DownloadFile(Socket socket, long size, string fileName)
        {
            long offset = 0;

            long parts = FileManager.GetParts(size);
            long currentPart = 1;

            while (offset < size)
            {
                byte[] data;
                if (currentPart == parts)
                {
                    var lastPartSize = (int)(size - offset);
                    data = Receive(socket, lastPartSize);
                    offset += lastPartSize;
                }
                else
                {
                    data = Receive(socket, HeaderConstants.MAX_PACKET_SIZE);
                    offset += HeaderConstants.MAX_PACKET_SIZE;
                }

                FileStreamManager.Write(fileName, data);
                currentPart++;
            }
        }
    }
}