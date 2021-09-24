using System;
using System.Text;
using Protocol;

namespace ConsoleClient.Function.Purchase
{
    public class DeletePurchaseFunction : FunctionTemplate
    {
        public const string NAME = "Desinstalar juego";
        
        public override DataPacket BuildRequest(string session)
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();

            var message = $"{session}&{title}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.DELETE_PURCHASE, message.Length);
            
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
                Console.WriteLine("Juego desinstalado exitosamente");
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public DeletePurchaseFunction()
        {
            base.Name = NAME;
        }
        
    }
}