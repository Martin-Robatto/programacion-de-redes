using Protocol;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Function.Review
{
    public class GetReviewsByGameFunction : FunctionTemplate
    {
        public const string NAME = "Reseñas de juego";

        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();

            var message = $"{base.session}&{title}";
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
                var reviews = data.Split("#");
                Console.WriteLine("Reseñas: ");
                Parallel.ForEach(reviews, review =>
                {
                    Console.WriteLine(review);
                });
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