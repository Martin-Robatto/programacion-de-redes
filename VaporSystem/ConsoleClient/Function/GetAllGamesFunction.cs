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
            throw new NotImplementedException();
        }
    }
}
