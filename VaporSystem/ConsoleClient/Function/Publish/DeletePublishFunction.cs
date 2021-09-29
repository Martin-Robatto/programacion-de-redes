using Protocol;
using System;
using System.Text;

namespace ConsoleClient.Function
{
    public class DeletePublishFunction : FunctionTemplate
    {
        public const string NAME = "Eliminar juego";

        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();

            var message = $"{base.session}&{title}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.DELETE_PUBLISH, message.Length);

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
                Console.WriteLine("Juego eliminado exitosamente");
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public DeletePublishFunction()
        {
            base.Name = NAME;
        }
    }
}