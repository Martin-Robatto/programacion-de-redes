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
        public static void Send(Socket socket, Header header, string message)
        {
            var data = header.GetRequest();

            var sentBytes = 0;
            while (sentBytes < data.Length)
            {
                sentBytes += socket.Send(data, sentBytes, data.Length - sentBytes, SocketFlags.None);
            }

            sentBytes = 0;
            var bytesMessage = Encoding.UTF8.GetBytes(message);
            while (sentBytes < bytesMessage.Length)
            {
                sentBytes += socket.Send(bytesMessage, sentBytes, bytesMessage.Length - sentBytes, SocketFlags.None);
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