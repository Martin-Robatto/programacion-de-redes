using System;
using System.Text;
using Protocol;

namespace ConsoleClient.Function.Review
{
    public class PostReviewFunction : FunctionTemplate
    {
        public const string NAME = "Calificar juego";
        
        public override DataPacket BuildRequest(string session)
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();
            Console.WriteLine("Ingrese la calificacion: ");
            var rate = Console.ReadLine();
            Console.WriteLine("Ingrese el comentario: ");
            var comment = Console.ReadLine();

            var message = $"{session}&{title}&{rate}#{comment}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.POST_REVIEW, message.Length);
            
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
                Console.WriteLine("Juego calificado exitosamente");
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public PostReviewFunction()
        {
            base.Name = NAME;
        }
    }
}