using System;
using System.Text;
using Protocol;

namespace ConsoleClient.Function.Review
{
    public class DeleteReviewFunction : FunctionTemplate
    {
        public const string NAME = "Eliminar reseña";
        
        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();

            var message = $"{base.session}&{title}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.DELETE_REVIEW, message.Length);
            
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
                Console.WriteLine("Reseña eliminada exitosamente");
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public DeleteReviewFunction()
        {
            base.Name = NAME;
        }
    }
}