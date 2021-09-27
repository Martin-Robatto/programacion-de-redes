using System;
using System.Net.Sockets;
using System.Text;
using FileLogic;
using Protocol;

namespace SocketLogic
{
    public class NetworkStreamManager
    {
        public static void Send(NetworkStream stream, DataPacket dataPacket)
        {
            var header = dataPacket.Header.GetRequest();
            stream.Write(header, 0, header.Length);

            var message = Encoding.UTF8.GetBytes(dataPacket.Payload);
            stream.Write(message, 0, message.Length);

            if (dataPacket.StatusCode != StatusCodeConstants.EMPTY)
            {
                var statusCode = Encoding.UTF8.GetBytes(dataPacket.StatusCode.ToString());
                stream.Write(statusCode, 0, statusCode.Length);
            }
        }

        public static byte[] Receive(NetworkStream stream, int length)
        {
            byte[] buffer = new byte[length];
            var totalReceived = 0;
            while (totalReceived < length)
            {
                var received = stream.Read(buffer, totalReceived, length - totalReceived);
                if (received == 0)
                {
                    throw new SocketException();
                }
                totalReceived += received;
            }
            return buffer;
        }

        public static void UploadFile(NetworkStream stream, long fileSize, string filePath)
        {
            long parts = FileManager.GetParts(fileSize);
            long offset = 0;
            long currentPart = 1;

            while (fileSize > offset)
            {
                byte[] data;
                if (currentPart == parts)
                {
                    var lastPartSize = (int)(fileSize - offset);
                    data = FileStreamManager.Read(filePath, offset, lastPartSize);
                    offset += lastPartSize;
                }
                else
                {
                    data = FileStreamManager.Read(filePath, offset, HeaderConstants.MAX_PACKET_SIZE);
                    offset += HeaderConstants.MAX_PACKET_SIZE;
                }
                stream.Write(data, 0, data.Length);
                currentPart++;
            }
        }
        
        public static void DownloadFile(NetworkStream stream, long fileSize, string fileName)
        {
            long parts = FileManager.GetParts(fileSize);
            long offset = 0;
            long currentPart = 1;

            while (fileSize > offset)
            {
                byte[] data;
                if (currentPart == parts)
                {
                    var lastPartSize = (int) (fileSize - offset);
                    data = Receive(stream, lastPartSize);
                    offset += lastPartSize;
                }
                else
                {
                    data = Receive(stream, HeaderConstants.MAX_PACKET_SIZE);
                    offset += HeaderConstants.MAX_PACKET_SIZE;
                }

                FileStreamManager.Write(fileName, data);
                currentPart++;
            }
        }
    }
}