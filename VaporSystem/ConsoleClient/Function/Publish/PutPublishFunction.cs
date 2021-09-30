using Protocol;
using System;
using System.Text;

namespace ConsoleClient.Function
{
    public class PutPublishFunction : FunctionTemplate
    {
        public const string NAME = "Modificar juego";

        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo titulo: ");
            var newTitle = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo genero: ");
            var newGenre = Console.ReadLine();
            Console.WriteLine("Ingrese la nueva sinopsis: ");
            var newSynopsis = Console.ReadLine();

            var message = $"{base.session}&{title}&{newTitle}#{newGenre}#{newSynopsis}";
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