using System;
using System.Text;
using Protocol;

namespace ConsoleClient.Function.Review
{
    public class GetReviewsByUserFunction : FunctionTemplate
    {
        public const string NAME = "Calificaciones";
        
        public override DataPacket BuildRequest(string session)
        {
            var message = session;
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.GET_REVIEWS_BY_USER, message.Length);
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
                Console.WriteLine("Reviews: ");
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

        public GetReviewsByUserFunction()
        {
            base.Name = NAME;
        }
    }
}