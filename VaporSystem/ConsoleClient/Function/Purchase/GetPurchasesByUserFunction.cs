using Protocol;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Function.Purchase
{
    public class GetPurchasesByUserFunction : FunctionTemplate
    {
        public const string NAME = "Biblioteca";

        public override DataPacket BuildRequest()
        {
            var message = session;
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.GET_PURCHASES_BY_USER, message.Length);
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
            var data = Encoding.UTF8.GetString(bufferData, HeaderConstants.STATUS_CODE_LENGTH,
                bufferData.Length - HeaderConstants.COMMAND_LENGTH - 1);
            if (statusCode == StatusCodeConstants.OK)
            {
                var purchases = data.Split("#");
                Console.WriteLine("Biblioteca: ");
                Parallel.ForEach(purchases, purchase =>
                {
                    Console.WriteLine(purchase);
                });
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public GetPurchasesByUserFunction()
        {
            base.Name = NAME;
        }
    }
}