using System;
using System.Net.Sockets;
using System.Text;
using Protocol;
using SocketLogic;

namespace ConsoleClient.Function
{
    public class RegisterFunction : FunctionTemplate
    {
        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el email: ");
            var email = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre: ");
            var username = Console.ReadLine();
            Console.WriteLine("Ingrese la contraseña: ");
            var password = Console.ReadLine();

            var message = $"{email}#{username}#{password}";
            var header = new Header(HeaderConstants.Request, FunctionConstants.Register, message.Length);
            
            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void SendRequest(Socket socket, DataPacket dataPacket = null)
        {
            SocketManager.Send(socket, dataPacket.Header, dataPacket.Payload);
        }

        public override void ReceiveResponse(Socket socket, Header header = null)
        {
            var bufferHeader = new byte[HeaderConstants.HeaderLength];
            try
            {
                SocketManager.Receive(socket, HeaderConstants.HeaderLength, bufferHeader);
                header = new Header();
                header.DecodeData(bufferHeader);
                var bufferData = new byte[header.DataLength];
                SocketManager.Receive(socket, header.DataLength, bufferData);
                Console.WriteLine("Respuesta: " + Encoding.UTF8.GetString(bufferData));
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
        }
    }
}