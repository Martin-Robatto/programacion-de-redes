using System;
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
                sentBytes += socket.Send(bytesMessage, sentBytes, bytesMessage.Length - sentBytes,  SocketFlags.None);
            }
        }

        public static void Receive(Socket socket,  int length, byte[] buffer)
        {
            var receive = 0;
            while (receive < length)
            {
                try
                {
                    var localReceive = socket.Receive(buffer, receive, length - receive, SocketFlags.None);
                    if (localReceive == 0)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        throw new ConnectionClosedException();
                    }
                    receive += localReceive;
                }
                catch (SocketException socketException)
                {
                    Console.WriteLine(socketException.Message);
                }
            }
        }
    }
}