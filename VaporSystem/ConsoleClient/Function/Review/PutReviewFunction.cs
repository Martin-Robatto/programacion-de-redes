using Protocol;
using System;
using System.Text;

namespace ConsoleClient.Function.Review
{
    public class PutReviewFunction : FunctionTemplate
    {
        public const string NAME = "Modificar reseña";

        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();
            Console.WriteLine("Ingrese la nueva calificacion [1-5]: ");
            var rate = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo comentario: ");
            var comment = Console.ReadLine();

            var message = $"{base.session}&{title}&{rate}#{comment}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.PUT_REVIEW, message.Length);

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
                Console.WriteLine("Reseña modificada exitosamentmente");
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public PutReviewFunction()
        {
            base.Name = NAME;
        }
    }
}