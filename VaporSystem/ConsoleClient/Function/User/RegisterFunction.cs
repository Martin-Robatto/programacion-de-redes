using Protocol;
using System;
using System.Text;

namespace ConsoleClient.Function
{
    public class RegisterFunction : FunctionTemplate
    {
        public const string NAME = "Registrar";

        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el nombre: ");
            var username = Console.ReadLine();
            Console.WriteLine("Ingrese la contraseña: ");
            var password = Console.ReadLine();

            var message = $"{username}#{password}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.REGISTER, message.Length);

            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void ProcessResponse(byte[] bufferData)
        {
            var statusCode = Int32.Parse(Encoding.UTF8.GetString(bufferData, 0, HeaderConstants.STATUS_CODE_LENGTH));
            var data = Encoding.UTF8.GetString(bufferData, HeaderConstants.STATUS_CODE_LENGTH, bufferData.Length - HeaderConstants.COMMAND_LENGTH - 1);
            if (statusCode == StatusCodeConstants.CREATED)
            {
                Console.WriteLine("Usuario creado exitosamente");
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public RegisterFunction()
        {
            base.Name = NAME;
        }
    }
}