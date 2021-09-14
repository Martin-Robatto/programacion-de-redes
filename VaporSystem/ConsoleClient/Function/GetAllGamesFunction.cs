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
        public override DataPacket BuildRequest()
        {
            var message = string.Empty;
            var header = new Header(HeaderConstants.Request, FunctionConstants.GetAllGames, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void ProcessResponse(byte[] bufferData)
        {
            String gamesLine = Encoding.UTF8.GetString(bufferData);
            var games = gamesLine.Split("#");
            Console.WriteLine("Games: ");
            foreach (String game in games)
            {
                Console.WriteLine(game);
            }
        }
    }
}
