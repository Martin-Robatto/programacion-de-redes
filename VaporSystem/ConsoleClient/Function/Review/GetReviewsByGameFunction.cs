using System;
using System.Text;
using Protocol;

namespace ConsoleClient.Function.Review
{
    public class GetReviewsByGameFunction : FunctionTemplate
    {
        public const string NAME = "Reseñas de juego";
        
        public override DataPacket BuildRequest(string session)
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();

            var message = $"{session}&{title}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.GET_REVIEWS_BY_GAME, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message,
                StatusCode = StatusCodeConstants.EMPTY
            };
        }
        
        public override void ProcessResponse(byte[] bufferData)
        {
            var statusCode = Int32.Parse(Encoding.UTF8.GetString(bufferData, 0, HeaderConstants.STATUS_CODE_LENGTH));
            var data = Encoding.UTF8.GetString(bufferData, HeaderConstants.STATUS_CODE_LENGTH, bufferData.Length - HeaderConstants.COMMAND_LENGTH - 1);
            if (statusCode == StatusCodeConstants.OK)
            {
                var publishes = data.Split("#");
                Console.WriteLine("Reseñas: ");
                foreach (String publish in publishes)
                {
                    Console.WriteLine(publish);
                }
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public GetReviewsByGameFunction()
        {
            base.Name = NAME;
        }
    }
}