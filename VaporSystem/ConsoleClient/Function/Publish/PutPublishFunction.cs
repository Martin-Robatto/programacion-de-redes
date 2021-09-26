using System;
using System.Text;
using Protocol;

namespace ConsoleClient.Function
{
    public class PutPublishFunction : FunctionTemplate
    {
        public const string NAME = "Modificar juego";
        
        public override DataPacket BuildRequest(string session)
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo genero: ");
            var genre = Console.ReadLine();
            Console.WriteLine("Ingrese la nueva sinopsis: ");
            var synopsis = Console.ReadLine();

            var message = $"{session}&{title}#{genre}#{synopsis}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.PUT_PUBLISH, message.Length);
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
            if (statusCode == StatusCodeConstants.OK)
            {
                Console.WriteLine("Juego modificado exitosamente");
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public PutPublishFunction()
        {
            base.Name = NAME;
        }
    }
}