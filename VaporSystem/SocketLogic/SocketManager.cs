using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Exceptions;
using Protocol;

namespace SocketLogic
{
    public class SocketManager
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
    }
}