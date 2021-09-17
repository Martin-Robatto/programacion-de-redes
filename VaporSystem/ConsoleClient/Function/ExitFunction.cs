using System.Net.Sockets;
using FunctionInterface;
using Protocol;

namespace ConsoleClient.Function
{
    public class ExitFunction : FunctionTemplate
    {
        public const string NAME = "Salir";

        public override DataPacket BuildRequest()
        {
            var message = string.Empty;
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.EXIT, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void ProcessResponse(byte[] bufferData)
        {
            ClientDisplay.Closing();
            ClientHandler.Exit = true;
        }

        public ExitFunction()
        {
            base.Name = NAME;
        }
    }
}