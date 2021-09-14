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
        public static void Send(Socket socket, DataPacket dataPacket)
        {
            var sentBytes = 0;
            var header = dataPacket.Header.GetRequest();
            while (sentBytes < header.Length)
            {
                sentBytes += socket.Send(header, sentBytes, header.Length - sentBytes, SocketFlags.None);
            }

            sentBytes = 0;
            var message = Encoding.UTF8.GetBytes(dataPacket.Payload);
            while (sentBytes < message.Length)
            {
                sentBytes += socket.Send(message, sentBytes, message.Length - sentBytes, SocketFlags.None);
            }
        }

        public static void Receive(Socket socket, int length, byte[] buffer)
        {
            var totalReceived = 0;
            while (totalReceived < length)
            {
                try
                {
                    var receive = socket.Receive(buffer, totalReceived, length - totalReceived, SocketFlags.None);
                    if (receive == 0)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        throw new ConnectionClosedException();
                    }
                    totalReceived += receive;
                }
                catch (SocketException socketException)
                {
                    Console.WriteLine(socketException.Message);
                }
            }
        }
    }
}