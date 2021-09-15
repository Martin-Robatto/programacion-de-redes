using System;
using System.Net.Sockets;
using System.Text;
using Protocol;
using SocketLogic;

namespace ConsoleClient.Function
{
    public class RegisterFunction : FunctionTemplate
    {
        public const string NAME = "Registrar";
        
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

        public override void ProcessResponse(byte[] bufferData) { }

        public RegisterFunction()
        {
            base.Name = NAME;
        }
    }
}