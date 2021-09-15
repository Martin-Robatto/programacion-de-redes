using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Function
{
    public class GetAllGamesFunction : FunctionTemplate
    {
        public const string NAME = "Juegos";
        
        public override DataPacket BuildRequest()
        {
            var message = string.Empty;
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.GET_ALL_GAMES, message.Length);
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
                var games = data.Split("#");
                Console.WriteLine("Games: ");
                foreach (String game in games)
                {
                    Console.WriteLine(game);
                }
            }
            else if (statusCode == StatusCodeConstants.SERVER_ERROR)
            {
                Console.WriteLine("Error de servidor");
            }
        }

        public GetAllGamesFunction()
        {
            base.Name = NAME;
        }
    }
}
